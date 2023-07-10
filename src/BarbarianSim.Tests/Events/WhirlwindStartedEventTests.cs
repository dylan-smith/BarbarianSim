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

    private readonly FakeRandomGenerator _fakeRandomGenerator = new();

    public WhirlwindStartedEventTests()
    {
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(1.0, DamageType.Physical, SkillType.Core));
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.0, DamageType.Physical));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(1.5));
        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(1.0));
        RandomGenerator.InjectMock(_fakeRandomGenerator);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 1.0);
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 1.0);
    }

    [Fact]
    public void Adds_DamageEvent_To_Queue()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Timestamp.Should().Be(123);
        damageEvent.DamageType.Should().Be(DamageType.Direct);
        damageEvent.DamageSource.Should().Be(DamageSource.Whirlwind);
        damageEvent.SkillType.Should().Be(SkillType.Core);
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

        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(4.5, DamageType.Physical, SkillType.Core));
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
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.7, DamageType.Physical));
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.69);
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
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.0);
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

        whirlwindStartedEvent.WeaponCooldownCompletedEvent.Should().NotBeNull();
        state.Events.Should().Contain(whirlwindStartedEvent.WeaponCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        whirlwindStartedEvent.WeaponCooldownCompletedEvent.Timestamp.Should().Be(124);
        whirlwindStartedEvent.WeaponCooldownCompletedEvent.Aura.Should().Be(Aura.WeaponCooldown);
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

        whirlwindStartedEvent.WeaponCooldownCompletedEvent.Timestamp.Should().Be(123.5);
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

        whirlwindStartedEvent.WeaponCooldownCompletedEvent.Timestamp.Should().Be(123.6);
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
        whirlwindStartedEvent.FurySpentEvent.SkillType.Should().Be(SkillType.Core);
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
        var config = new SimulationConfig()
        {
            Skills = { [Skill.Whirlwind] = 1 },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

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
        var config = new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.7));
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 1.0, 0.69);


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
        var config = new SimulationConfig()
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.EnhancedWhirlwind] = 1, },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

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
        var config = new SimulationConfig()
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.EnhancedWhirlwind] = 1, },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        config.EnemySettings.IsElite = true;
        var state = new SimulationState(config);
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.FuryGeneratedEvents[0].BaseFury.Should().Be(4);
        whirlwindStartedEvent.FuryGeneratedEvents[1].BaseFury.Should().Be(4);
    }

    [Fact]
    public void FuriousWhirlwind_Applies_Bleeds()
    {
        var config = new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.FuriousWhirlwind] = 1, },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        config.Gear.TwoHandSlashing.MinDamage = 1000;
        config.Gear.TwoHandSlashing.MaxDamage = 1000;
        config.Gear.TwoHandSlashing.AttacksPerSecond = 1;
        var state = new SimulationState(config);
        Whirlwind.Weapon = config.Gear.TwoHandSlashing;

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.BleedAppliedEvents[0]);
        state.Events.Should().Contain(whirlwindStartedEvent.BleedAppliedEvents[1]);
        state.Events.Count(e => e is BleedAppliedEvent).Should().Be(2);
        whirlwindStartedEvent.BleedAppliedEvents[0].Damage.Should().Be(400);
        whirlwindStartedEvent.BleedAppliedEvents[1].Damage.Should().Be(400);
        whirlwindStartedEvent.BleedAppliedEvents[0].Timestamp.Should().Be(123);
        whirlwindStartedEvent.BleedAppliedEvents[1].Timestamp.Should().Be(123);
        whirlwindStartedEvent.BleedAppliedEvents[0].Duration.Should().Be(5);
        whirlwindStartedEvent.BleedAppliedEvents[1].Duration.Should().Be(5);
    }

    [Fact]
    public void FuriousWhirlwind_Applies_Bleeds_Including_Damage_Bonuses()
    {
        var config = new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.FuriousWhirlwind] = 1, },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        config.EnemySettings.NumberOfEnemies = 2;
        config.Gear.TwoHandSlashing.MinDamage = 1000;
        config.Gear.TwoHandSlashing.MaxDamage = 1000;
        config.Gear.TwoHandSlashing.AttacksPerSecond = 1;
        var state = new SimulationState(config);
        Whirlwind.Weapon = config.Gear.TwoHandSlashing;
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(3.5, DamageType.Physical, SkillType.Core));

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.BleedAppliedEvents[0].Damage.Should().Be(1400);
        whirlwindStartedEvent.BleedAppliedEvents[1].Damage.Should().Be(1400);
    }

    [Fact]
    public void FuriousWhirlwind_Does_Not_Proc_If_Not_Using_Slashing_Weapon()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.FuriousWhirlwind] = 1, },
        });
        state.Config.EnemySettings.NumberOfEnemies = 2;
        state.Config.EnemySettings.NumberOfEnemies = 2;
        state.Config.Gear.TwoHandBludgeoning.MinDamage = 1000;
        state.Config.Gear.TwoHandBludgeoning.MaxDamage = 1000;
        state.Config.Gear.TwoHandBludgeoning.AttacksPerSecond = 1;
        Whirlwind.Weapon = state.Config.Gear.TwoHandBludgeoning;
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(3.5, DamageType.Physical, SkillType.Core));

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().NotContain(e => e is BleedAppliedEvent);
    }

    [Fact]
    public void ViolentWhirlwind_Creates_ViolentWhirlwindAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.ViolentWhirlwind] = 1, },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.ViolentWhirlwindAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ViolentWhirlwind);
        whirlwindStartedEvent.ViolentWhirlwindAppliedEvent.Timestamp.Should().Be(125);
    }

    [Fact]
    public void When_ViolentWhirlwind_Active_Damage_Increased_By_30_Percent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, [Skill.ViolentWhirlwind] = 1, },
        });
        state.Player.Auras.Add(Aura.ViolentWhirlwind);
        Whirlwind.Weapon = new GearItem { MinDamage = 1000, MaxDamage = 1000, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindStartedEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.DamageEvents.First().Damage.Should().Be(221); // 1000 [WeaponDmg] * 0.17 [SkillModifier] * 1.3 [ViolentWhirlwind]
    }

    [Fact]
    public void LuckyHit_20_Percent_Chance()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1000, MaxDamage = 1000, AttacksPerSecond = 1 };
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 0.19);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.LuckyHitEvents.Single());
        state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
        whirlwindStartedEvent.LuckyHitEvents.Single().Timestamp.Should().Be(123);
        whirlwindStartedEvent.LuckyHitEvents.Single().SkillType.Should().Be(SkillType.Core);
        whirlwindStartedEvent.LuckyHitEvents.Single().Target.Should().Be(state.Enemies.First());
    }

    [Fact]
    public void LuckyHit_Includes_Increased_Chance_From_Gear()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1, },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1000, MaxDamage = 1000, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(LuckyHitChanceCalculator), new FakeStatCalculator(15));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 0.34);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
    }

    [Fact]
    public void Multiple_Enemies_Can_Have_Lucky_Hits_For_Each()
    {
        var config = new SimulationConfig()
        {
            Skills = { [Skill.Whirlwind] = 1, },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        Whirlwind.Weapon = new GearItem { MinDamage = 1000, MaxDamage = 1000, AttacksPerSecond = 1 };
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 0.19);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.LuckyHitEvents);
        state.Events.Count(e => e is LuckyHitEvent).Should().Be(2);
        whirlwindStartedEvent.LuckyHitEvents.First().Target.Should().Be(state.Enemies.First());
        whirlwindStartedEvent.LuckyHitEvents.Last().Target.Should().Be(state.Enemies.Last());
    }

    [Fact]
    public void Uses_Weapon_Expertise_For_CritDamageCalculator()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1, Expertise = Expertise.TwoHandedAxe };
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.0);
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(3.5, Expertise.TwoHandedAxe));
        var whirlwindStartedEvent = new WhirlwindStartedEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.DamageEvents.First().Damage.Should().BeApproximately(0.595, 0.0000001); // 1 [Weapon Damage] * 0.17 [Skill Multiplier] * 3.5 [Crit Damage]
    }
}
