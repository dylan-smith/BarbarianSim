using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class WhirlwindSpinEventHandlerTests
{
    private readonly Mock<AttackSpeedCalculator> _mockAttackSpeedCalculator = TestHelpers.CreateMock<AttackSpeedCalculator>();
    private readonly Mock<Whirlwind> _mockWhirlwind = TestHelpers.CreateMock<Whirlwind>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WhirlwindSpinEventHandler _handler;

    public WhirlwindSpinEventHandlerTests()
    {
        _mockAttackSpeedCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                                  .Returns(1.0);
        _mockWhirlwind.Setup(m => m.GetSkillMultiplier(It.IsAny<SimulationState>()))
                      .Returns(0.17);

        _state.Config.PlayerSettings.SkillWeapons.Add(Skill.Whirlwind, new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 });

        _handler = new WhirlwindSpinEventHandler(_mockAttackSpeedCalculator.Object, _mockWhirlwind.Object);
    }

    [Fact]
    public void Averages_Weapon_Min_Max_Damage()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind] = new GearItem { MinDamage = 300, MaxDamage = 500, AttacksPerSecond = 1 };

        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        whirlwindSpinEvent.DirectDamageEvents.Single().BaseDamage.Should().Be(400 * 0.17); // 400 [WeaponDmg] * 0.17 [SkillModifier]
    }

    [Fact]
    public void Applies_Skill_Multiplier()
    {
        _mockWhirlwind.Setup(m => m.GetSkillMultiplier(It.IsAny<SimulationState>()))
                      .Returns(0.23);
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        whirlwindSpinEvent.DirectDamageEvents.Single().BaseDamage.Should().Be(1.5 * 0.23); // 1.5 [WeaponDmg] * 0.23 [SkillModifier]
    }

    [Fact]
    public void Creates_DirectDamageEvent()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind] = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1, Expertise = Expertise.TwoHandedSword };

        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        _state.Events.Should().Contain(whirlwindSpinEvent.DirectDamageEvents.First());
        _state.Events.Should().ContainSingle(e => e is DirectDamageEvent);
        whirlwindSpinEvent.DirectDamageEvents.First().Timestamp.Should().Be(123.0);
        whirlwindSpinEvent.DirectDamageEvents.First().BaseDamage.Should().Be(1.5 * 0.17);
        whirlwindSpinEvent.DirectDamageEvents.First().DamageType.Should().Be(DamageType.Physical);
        whirlwindSpinEvent.DirectDamageEvents.First().DamageSource.Should().Be(DamageSource.Whirlwind);
        whirlwindSpinEvent.DirectDamageEvents.First().SkillType.Should().Be(SkillType.Core);
        whirlwindSpinEvent.DirectDamageEvents.First().LuckyHitChance.Should().Be(0.2);
        whirlwindSpinEvent.DirectDamageEvents.First().Expertise.Should().Be(Expertise.TwoHandedSword);
        whirlwindSpinEvent.DirectDamageEvents.First().Enemy.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void Creates_DirectDamageEvent_For_Each_Enemy()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        state.Config.PlayerSettings.SkillWeapons.Add(Skill.Whirlwind, new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 });

        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, state);

        state.Events.Should().Contain(whirlwindSpinEvent.DirectDamageEvents[0]);
        state.Events.Should().Contain(whirlwindSpinEvent.DirectDamageEvents[1]);
        state.Events.Count(e => e is DirectDamageEvent).Should().Be(2);
    }

    [Fact]
    public void Creates_WeaponAuraCooldownAuraAppliedEvent()
    {
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        whirlwindSpinEvent.WeaponCooldownAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(whirlwindSpinEvent.WeaponCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WeaponCooldown);
        whirlwindSpinEvent.WeaponCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        whirlwindSpinEvent.WeaponCooldownAuraAppliedEvent.Aura.Should().Be(Aura.WeaponCooldown);
        whirlwindSpinEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(1);
    }

    [Fact]
    public void Considers_Weapon_AttacksPerSecond_When_Creating_WeaponAuraCooldownAuraAppliedEvent()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind] = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 2 };
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        whirlwindSpinEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.5);
    }

    [Fact]
    public void Considers_AttackSpeed_Bonuses_When_Creating_WeaponAuraCooldownCompletedEvent()
    {
        _mockAttackSpeedCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                                  .Returns(0.6);

        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        whirlwindSpinEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.6);
    }

    [Fact]
    public void Creates_FurySpentEvent()
    {
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        _state.Events.Should().Contain(whirlwindSpinEvent.FurySpentEvent);
        _state.Events.Should().ContainSingle(e => e is FurySpentEvent);
        whirlwindSpinEvent.FurySpentEvent.Timestamp.Should().Be(123);
        whirlwindSpinEvent.FurySpentEvent.BaseFurySpent.Should().Be(25.0);
        whirlwindSpinEvent.FurySpentEvent.SkillType.Should().Be(SkillType.Core);
    }

    [Fact]
    public void Creates_WhirlwindRefreshEvent()
    {
        var whirlwindSpinEvent = new WhirlwindSpinEvent(123.0);

        _handler.ProcessEvent(whirlwindSpinEvent, _state);

        _state.Events.Should().Contain(whirlwindSpinEvent.WhirlwindRefreshEvent);
        _state.Events.Should().ContainSingle(e => e is WhirlwindRefreshEvent);
        whirlwindSpinEvent.WhirlwindRefreshEvent.Timestamp.Should().Be(124.0);
    }
}
