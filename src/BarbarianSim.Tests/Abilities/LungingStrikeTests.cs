using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public sealed class LungingStrikeTests : IDisposable
{
    public void Dispose()
    {
        BaseStatCalculator.ClearMocks();
        RandomGenerator.ClearMock();
    }

    public LungingStrikeTests()
    {
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.0));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(1.5));
        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(1.0));
    }

    [Fact]
    public void CanUse_When_Weapon_On_Cooldown_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WeaponCooldown);

        LungingStrike.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_When_Weapon_Not_On_Cooldown_Returns_True()
    {
        var state = new SimulationState(new SimulationConfig());

        LungingStrike.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void Use_Adds_LungingStrikeEvent_To_Events()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        LungingStrike.Use(state);

        state.Events.Count.Should().Be(1);
        state.Events[0].Should().BeOfType<LungingStrikeEvent>();
        state.Events[0].Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(1, 0.33)]
    [InlineData(2, 0.36)]
    [InlineData(3, 0.39)]
    [InlineData(4, 0.42)]
    [InlineData(5, 0.45)]
    [InlineData(6, 0.45)]
    public void GetSkillMultiplier_Converts_Skill_Points_To_Correct_Multiplier(int skillPoints, double expectedMultiplier)
    {
        var state = new SimulationState(new SimulationConfig { Skills = { [Skill.LungingStrike] = skillPoints } });

        var result = LungingStrike.GetSkillMultiplier(state);

        result.Should().Be(expectedMultiplier);
    }

    [Fact]
    public void GetSkillMultiplier_Includes_Skill_Points_And_Gear_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });

        state.Config.Gear.Helm.LungingStrike = 2;

        var result = LungingStrike.GetSkillMultiplier(state);

        result.Should().Be(0.39);
    }

    [Fact]
    public void Adds_DamageEvent_To_Queue()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Timestamp.Should().Be(123);
        damageEvent.DamageType.Should().Be(DamageType.Direct);
        damageEvent.Damage.Should().Be(0.33); // 1 [WeaponDmg] * 0.33 [SkillModifier]
    }

    [Fact]
    public void Averages_Weapon_Min_Max_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 300, MaxDamage = 500, AttacksPerSecond = 1 };

        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().Be(132); // 400 [WeaponDmg] * 0.33 [SkillModifier]
    }

    [Fact]
    public void Applies_Skill_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 4 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().Be(0.42); // 1 [WeaponDmg] * 0.42 [SkillModifier]
    }

    [Fact]
    public void Applies_TotalDamageMultiplier_To_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(4.5));
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 1.0));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().Be(1.485); // 1 [WeaponDmg] * 4.5 [DmgMultiplier] * 0.33 [SkillModifier]
    }

    [Fact]
    public void Critical_Strike_Applies_CritChance_Bonus()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.7));
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 0.69));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Timestamp.Should().Be(123);
        damageEvent.DamageType.Should().Be(DamageType.DirectCrit);
        damageEvent.Damage.Should().Be(0.495); // 1 [WeaponDmg] * 0.33 [SkillModifier] * 1.5 [Crit]
    }

    [Fact]
    public void Critical_Strike_Applies_CritDamaage_Bonus()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.CriticalStrike, 0.0));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(3.5));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Damage.Should().Be(1.155); // 1 [WeaponDmg] * 0.33 [SkillModifier] * 3.5 [Crit]
    }

    [Fact]
    public void Adds_WeaponAuraCooldownCompletedEvent_To_Queue()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent).Should().BeTrue();
        state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp.Should().Be(124);
    }

    [Fact]
    public void Considers_Weapon_AttacksPerSecond_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 2 };

        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent).Should().BeTrue();
        state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp.Should().Be(123.5);
    }

    [Fact]
    public void Considers_AttackSpeed_Bonuses_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(0.6));

        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Any(e => e is WeaponAuraCooldownCompletedEvent).Should().BeTrue();
        state.Events.Single(e => e is WeaponAuraCooldownCompletedEvent).Timestamp.Should().Be(123.6);
    }
}
