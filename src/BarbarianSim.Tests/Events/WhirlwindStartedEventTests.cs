using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class WhirlwindStartedEventTests : IDisposable
{
    public void Dispose()
    {
        BaseStatCalculator.ClearMocks();
        RandomGenerator.ClearMock();
    }

    public WhirlwindStartedEventTests()
    {
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(1.5));
        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(1.0));
    }

    [Fact]
    public void Adds_DamageEvent_To_Queue()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Timestamp.Should().Be(123);
        damageEvent.DamageType.Should().Be(DamageType.Direct);
        damageEvent.DamageSource.Should().Be(DamageSource.Whirlwind);
        damageEvent.Damage.Should().Be(0.17); // 1 [WeaponDmg] * 0.17 [SkillModifier]
    }

    [Fact]
    public void Averages_Weapon_Min_Max_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 300, MaxDamage = 500, AttacksPerSecond = 1 };

        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().Be(68.0); // 400 [WeaponDmg] * 0.17 [SkillModifier]
    }

    [Fact]
    public void Applies_Skill_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 4 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().Be(0.23); // 1 [WeaponDmg] * 0.23 [SkillModifier]
    }

    [Fact]
    public void Applies_TotalDamageMultiplier_To_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(4.5));
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().Be(0.765); // 1 [WeaponDmg] * 4.5 [DmgMultiplier] * 0.17 [SkillModifier]
    }

    [Fact]
    public void Critical_Strike_Applies_CritChance_Bonus()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.7));
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 0.69));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Timestamp.Should().Be(123);
        damageEvent.DamageType.Should().Be(DamageType.DirectCrit);
        damageEvent.Damage.Should().Be(0.255); // 1 [WeaponDmg] * 0.17 [SkillModifier] * 1.5 [Crit]
    }

    [Fact]
    public void Critical_Strike_Applies_CritDamaage_Bonus()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 0.0));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(3.5));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().BeApproximately(0.595, 0.0000001); // 1 [WeaponDmg] * 0.17 [SkillModifier] * 3.5 [Crit]
    }

    [Fact]
    public void Adds_WeaponAuraCooldownCompletedEvent_To_Queue()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent).Should().BeTrue();
        state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp.Should().Be(124);
    }

    [Fact]
    public void Considers_Weapon_AttacksPerSecond_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 2 };

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent).Should().BeTrue();
        state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp.Should().Be(123.5);
    }

    [Fact]
    public void Considers_AttackSpeed_Bonuses_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(0.6));

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent).Should().BeTrue();
        state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp.Should().Be(123.6);
    }

    [Fact]
    public void Creates_FurySpentEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.FurySpentEvent);
        state.Events.Should().ContainSingle(e => e is FurySpentEvent);
        whirlwindStartedEvent.FurySpentEvent.Timestamp.Should().Be(123);
        whirlwindStartedEvent.FurySpentEvent.BaseFurySpent.Should().Be(25.0);
    }

    [Fact]
    public void Creates_WhirlwindRefreshEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.WhirlwindRefreshEvent);
        state.Events.Should().ContainSingle(e => e is WhirlwindRefreshEvent);
        whirlwindStartedEvent.WhirlwindRefreshEvent.Timestamp.Should().Be(124.0);
    }

    [Fact]
    public void Creates_DamageEvent_For_Each_Enemy()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        state.Config.EnemySettings.NumberOfEnemies = 2;
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0, 1.0));

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.DamageEvents[0]);
        state.Events.Should().Contain(whirlwindStartedEvent.DamageEvents[1]);
        state.Events.Count(e => e is DamageEvent).Should().Be(2);
        whirlwindStartedEvent.DamageEvents[0].Timestamp.Should().Be(123.0);
        whirlwindStartedEvent.DamageEvents[1].Timestamp.Should().Be(123.0);
    }

    [Fact]
    public void Separate_Crit_Roll_For_Each_Enemy()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        state.Config.EnemySettings.NumberOfEnemies = 2;
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.7));
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0, 0.69));

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.DamageEvents[0]);
        state.Events.Should().Contain(whirlwindStartedEvent.DamageEvents[1]);
        state.Events.Count(e => e is DamageEvent).Should().Be(2);
        whirlwindStartedEvent.DamageEvents[0].DamageType.Should().Be(DamageType.Direct);
        whirlwindStartedEvent.DamageEvents[1].DamageType.Should().Be(DamageType.DirectCrit);
    }

    [Fact]
    public void EnhancedWhirlwind_Generates_1_Fury_For_Non_Elites()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.EnhancedWhirlwind] = 1, },
        });
        state.Config.EnemySettings.NumberOfEnemies = 2;
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0, 1.0));

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.FuryGeneratedEvents[0]);
        state.Events.Should().Contain(whirlwindStartedEvent.FuryGeneratedEvents[1]);
        state.Events.Count(e => e is FuryGeneratedEvent).Should().Be(2);
        whirlwindStartedEvent.FuryGeneratedEvents[0].BaseFury.Should().Be(1);
        whirlwindStartedEvent.FuryGeneratedEvents[1].BaseFury.Should().Be(1);
        whirlwindStartedEvent.FuryGeneratedEvents[0].Timestamp.Should().Be(123);
        whirlwindStartedEvent.FuryGeneratedEvents[1].Timestamp.Should().Be(123);
    }

    [Fact]
    public void EnhancedWhirlwind_Generates_4_Fury_For_Elites()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.EnhancedWhirlwind] = 1, },
        });
        state.Config.EnemySettings.NumberOfEnemies = 2;
        state.Config.EnemySettings.IsElite = true;
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0, 1.0));

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.FuryGeneratedEvents[0].BaseFury.Should().Be(4);
        whirlwindStartedEvent.FuryGeneratedEvents[1].BaseFury.Should().Be(4);
    }
}
