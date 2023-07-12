using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class WhirlwindSpinEventTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public WhirlwindSpinEventTests() => BaseStatCalculator.InjectMock(typeof(AttackSpeedCalculator), new FakeStatCalculator(1.0));

    [Fact]
    public void Averages_Weapon_Min_Max_Damage()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 300, MaxDamage = 500, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindSpinEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.DirectDamageEvents.Single().BaseDamage.Should().Be(68); // 400 [WeaponDmg] * 0.17 [SkillModifier]
    }

    [Fact]
    public void Applies_Skill_Multiplier()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 4 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindSpinEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.DirectDamageEvents.Single().BaseDamage.Should().Be(0.23); // 1 [WeaponDmg] * 0.23 [SkillModifier]
    }

    [Fact]
    public void Creates_DirectDamageEvent()
    {
        var config = new SimulationConfig()
        {
            Skills = { [Skill.Whirlwind] = 1 },
        };
        var state = new SimulationState(config);
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1, Expertise = Expertise.TwoHandedSword };

        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        whirlwindSpinEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindSpinEvent.DirectDamageEvents.First());
        state.Events.Should().ContainSingle(e => e is DirectDamageEvent);
        whirlwindSpinEvent.DirectDamageEvents.First().Timestamp.Should().Be(123.0);
        whirlwindSpinEvent.DirectDamageEvents.First().BaseDamage.Should().Be(1.5 * 0.17);
        whirlwindSpinEvent.DirectDamageEvents.First().DamageType.Should().Be(DamageType.Physical);
        whirlwindSpinEvent.DirectDamageEvents.First().DamageSource.Should().Be(DamageSource.Whirlwind);
        whirlwindSpinEvent.DirectDamageEvents.First().SkillType.Should().Be(SkillType.Core);
        whirlwindSpinEvent.DirectDamageEvents.First().LuckyHitChance.Should().Be(0.2);
        whirlwindSpinEvent.DirectDamageEvents.First().Expertise.Should().Be(Expertise.TwoHandedSword);
        whirlwindSpinEvent.DirectDamageEvents.First().Enemy.Should().Be(state.Enemies.First());
    }

    [Fact]
    public void Creates_DirectDamageEvent_For_Each_Enemy()
    {
        var config = new SimulationConfig()
        {
            Skills = { [Skill.Whirlwind] = 1 },
        };
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        whirlwindSpinEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindSpinEvent.DirectDamageEvents[0]);
        state.Events.Should().Contain(whirlwindSpinEvent.DirectDamageEvents[1]);
        state.Events.Count(e => e is DirectDamageEvent).Should().Be(2);
    }

    [Fact]
    public void Creates_WeaponAuraCooldownAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindSpinEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.WeaponCooldownAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(whirlwindStartedEvent.WeaponCooldownAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WeaponCooldown);
        whirlwindStartedEvent.WeaponCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        whirlwindStartedEvent.WeaponCooldownAuraAppliedEvent.Aura.Should().Be(Aura.WeaponCooldown);
        whirlwindStartedEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(1);
    }

    [Fact]
    public void Considers_Weapon_AttacksPerSecond_When_Creating_WeaponAuraCooldownAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 2 };

        var whirlwindStartedEvent = new WhirlwindSpinEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.5);
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

        var whirlwindStartedEvent = new WhirlwindSpinEvent(123);

        whirlwindStartedEvent.ProcessEvent(state);

        whirlwindStartedEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.6);
    }

    [Fact]
    public void Creates_FurySpentEvent()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });
        Whirlwind.Weapon = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var whirlwindStartedEvent = new WhirlwindSpinEvent(123);

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

        var whirlwindStartedEvent = new WhirlwindSpinEvent(123.0);

        whirlwindStartedEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindStartedEvent.WhirlwindRefreshEvent);
        state.Events.Should().ContainSingle(e => e is WhirlwindRefreshEvent);
        whirlwindStartedEvent.WhirlwindRefreshEvent.Timestamp.Should().Be(124.0);
    }
}
