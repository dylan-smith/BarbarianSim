using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class LungingStrikeEventTests : IDisposable
{
    public void Dispose()
    {
        BaseStatCalculator.ClearMocks();
        RandomGenerator.ClearMock();
    }

    private readonly FakeRandomGenerator _fakeRandomGenerator = new();

    public LungingStrikeEventTests()
    {
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.0));
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
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Any(e => e is DamageEvent).Should().BeTrue();
        var damageEvent = (DamageEvent)state.Events.Single(e => e is DamageEvent);
        damageEvent.Timestamp.Should().Be(123);
        damageEvent.DamageType.Should().Be(DamageType.Direct);
        damageEvent.DamageSource.Should().Be(DamageSource.LungingStrike);
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
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.69);
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
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.0);
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

    [Fact]
    public void Creates_GenerateFuryEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.FuryGeneratedEvent);
        state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        lungingStrikeEvent.FuryGeneratedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.FuryGeneratedEvent.BaseFury.Should().Be(10);
    }

    [Fact]
    public void EnhancedLungingStrike_Adds_30_Percent_Damage_When_Enemy_Healthy()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.EnhancedLungingStrike] = 1 },
        });
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 1000;
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.DamageEvent.Damage.Should().BeApproximately(0.429, 0.0000001); // 1 [WeaponDmg] * 0.33 [SkillModifier] * 1.3 [EnhancedLungingStrike]
    }

    [Fact]
    public void EnhancedLungingStrike_Does_Not_Add_Damage_When_Enemy_Not_Healthy()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.EnhancedLungingStrike] = 1 },
        });
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 600;
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.DamageEvent.Damage.Should().Be(0.33); // 1 [WeaponDmg] * 0.33 [SkillModifier]
    }

    [Fact]
    public void EnhancedLungingStrike_Creates_HealingEvent_When_Enemy_Healthy()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.EnhancedLungingStrike] = 1 },
        });
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 1000;
        state.Player.MaxLife = 1000;
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.HealingEvent);
        state.Events.Should().ContainSingle(e => e is HealingEvent);
        lungingStrikeEvent.HealingEvent.BaseAmountHealed.Should().Be(20);
        lungingStrikeEvent.HealingEvent.Timestamp.Should().Be(123);
    }

    [Fact]
    public void EnhancedLungingStrike_Does_Not_Create_HealingEvent_When_Enemy_Not_Healthy()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.EnhancedLungingStrike] = 1 },
        });
        state.Enemies.First().MaxLife = 1000;
        state.Enemies.First().Life = 600;
        state.Player.MaxLife = 1000;
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.HealingEvent.Should().BeNull();
        state.Events.Should().NotContain(e => e is HealingEvent);
    }

    [Fact]
    public void CombatLungingStrike_Grants_Berserking_On_Crit()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.CombatLungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.0);
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.BerserkingAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BerserkingAppliedEvent);
        lungingStrikeEvent.BerserkingAppliedEvent.Duration.Should().Be(1.5);
    }

    [Fact]
    public void BattleLungingStrike_Applies_Bleed()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.BattleLungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.BleedAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        lungingStrikeEvent.BleedAppliedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.BleedAppliedEvent.Duration.Should().Be(5);
        lungingStrikeEvent.BleedAppliedEvent.Damage.Should().Be(0.066); // 1 [Weapon Damage] * 0.33 [Skill Modifier] * 0.2 [Battle Lunging Strike]
    }

    [Fact]
    public void BattleLungingStrike_Bleed_Damage_Includes_Damage_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.BattleLungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(2.3));
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.BleedAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        lungingStrikeEvent.BleedAppliedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.BleedAppliedEvent.Duration.Should().Be(5);
        lungingStrikeEvent.BleedAppliedEvent.Damage.Should().BeApproximately(0.1518, 0.0000001); // 1 [Weapon Damage] * 0.33 [Skill Modifier] * 2.3 [Damage Bonuses] * 0.2 [Battle Lunging Strike]
    }

    [Fact]
    public void BattleLungingStrike_Bleed_Damage_Does_Not_Includes_Crit_Bonus()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, [Skill.BattleLungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 0.0);
        var lungingStrikeEvent = new LungingStrikeEvent(123);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.BleedAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BleedAppliedEvent);
        lungingStrikeEvent.BleedAppliedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.BleedAppliedEvent.Duration.Should().Be(5);
        lungingStrikeEvent.BleedAppliedEvent.Damage.Should().Be(0.066); // 1 [Weapon Damage] * 0.33 [Skill Modifier] * 0.2 [Battle Lunging Strike]
    }

    [Fact]
    public void LuckyHit_50_Percent_Chance()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1000, MaxDamage = 1000, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 0.49);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.LuckyHitEvent);
        state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
        lungingStrikeEvent.LuckyHitEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.LuckyHitEvent.SkillType.Should().Be(SkillType.Basic);
        lungingStrikeEvent.LuckyHitEvent.Target.Should().Be(state.Enemies.First());
    }

    [Fact]
    public void LuckyHit_Includes_Increased_Chance_From_Gear()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1, },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1000, MaxDamage = 1000, AttacksPerSecond = 1 };
        BaseStatCalculator.InjectMock(typeof(LuckyHitChanceCalculator), new FakeStatCalculator(20));
        var lungingStrikeEvent = new LungingStrikeEvent(123);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 0.69);

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().ContainSingle(e => e is LuckyHitEvent);
    }
}
