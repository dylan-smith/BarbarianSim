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
        BaseStatCalculator.InjectMock(typeof(TotalDamageMultiplierCalculator), new FakeStatCalculator(1.0, DamageType.Physical | DamageType.Direct, SkillType.Basic));
        BaseStatCalculator.InjectMock(typeof(CritChanceCalculator), new FakeStatCalculator(0.0, DamageType.Physical | DamageType.Direct));
        BaseStatCalculator.InjectMock(typeof(CritDamageCalculator), new FakeStatCalculator(1.5));
        BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(1.0));
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));
        RandomGenerator.InjectMock(_fakeRandomGenerator);
        _fakeRandomGenerator.FakeRoll(RollType.LuckyHit, 1.0);
        _fakeRandomGenerator.FakeRoll(RollType.CriticalStrike, 1.0);
    }

    [Fact]
    public void Creates_DirectDamageEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1000, MaxDamage = 2000, AttacksPerSecond = 1, Expertise = Expertise.Polearm };
        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First());

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.DirectDamageEvent.Should().NotBeNull();
        state.Events.Should().ContainSingle(e => e is DirectDamageEvent);
        lungingStrikeEvent.DirectDamageEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.DirectDamageEvent.BaseDamage.Should().Be(495); // 1500 [WeaponDmg] * 0.33 [SkillModifier]
        lungingStrikeEvent.DirectDamageEvent.DamageType.Should().Be(DamageType.Physical);
        lungingStrikeEvent.DirectDamageEvent.DamageSource.Should().Be(DamageSource.LungingStrike);
        lungingStrikeEvent.DirectDamageEvent.SkillType.Should().Be(SkillType.Basic);
        lungingStrikeEvent.DirectDamageEvent.LuckyHitChance.Should().Be(0.5);
        lungingStrikeEvent.DirectDamageEvent.Expertise.Should().Be(Expertise.Polearm);
        lungingStrikeEvent.DirectDamageEvent.Enemy.Should().Be(state.Enemies.First());
    }

    [Fact]
    public void Averages_Weapon_Min_Max_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 300, MaxDamage = 500, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First());

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.DirectDamageEvent.BaseDamage.Should().Be(132); // 400 [WeaponDmg] * 0.33 [SkillModifier]
    }

    [Fact]
    public void Applies_Skill_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 4 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First());

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.DirectDamageEvent.BaseDamage.Should().Be(0.42); // 1 [WeaponDmg] * 0.42 [SkillModifier]
    }

    [Fact]
    public void Creates_WeaponAuraCooldownAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First());

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(lungingStrikeEvent.WeaponCooldownAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WeaponCooldown);
        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(1.0);
        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Aura.Should().Be(Aura.WeaponCooldown);
    }

    [Fact]
    public void Considers_Weapon_AttacksPerSecond_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 2 };

        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First());

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.5);
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

        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First());

        lungingStrikeEvent.ProcessEvent(state);

        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.6);
    }

    [Fact]
    public void Creates_GenerateFuryEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });
        LungingStrike.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123, state.Enemies.First());

        lungingStrikeEvent.ProcessEvent(state);

        state.Events.Should().Contain(lungingStrikeEvent.FuryGeneratedEvent);
        state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        lungingStrikeEvent.FuryGeneratedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.FuryGeneratedEvent.BaseFury.Should().Be(10);
    }
}
