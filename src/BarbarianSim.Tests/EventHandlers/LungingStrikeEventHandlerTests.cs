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

public class LungingStrikeEventHandlerTests
{
    private readonly Mock<AttackSpeedCalculator> _mockAttackSpeedCalculator = TestHelpers.CreateMock<AttackSpeedCalculator>();
    private readonly Mock<LungingStrike> _mockLungingStrike = TestHelpers.CreateMock<LungingStrike>();

    private readonly SimulationState _state = new SimulationState(new SimulationConfig());

    private readonly LungingStrikeEventHandler _handler;

    public LungingStrikeEventHandlerTests()
    {
        _mockAttackSpeedCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>()))
                                  .Returns(1.0);

        _mockLungingStrike.Setup(x => x.GetSkillMultiplier(It.IsAny<SimulationState>()))
                          .Returns(0.33);

        _handler = new LungingStrikeEventHandler(_mockAttackSpeedCalculator.Object, _mockLungingStrike.Object);
    }

    [Fact]
    public void Creates_DirectDamageEvent()
    {
        var skillWeapon = new GearItem { MinDamage = 1000, MaxDamage = 2000, AttacksPerSecond = 1, Expertise = Expertise.Polearm };
        _state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike] = skillWeapon;
        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(lungingStrikeEvent, _state);

        lungingStrikeEvent.DirectDamageEvent.Should().NotBeNull();
        _state.Events.Should().ContainSingle(e => e is DirectDamageEvent);
        lungingStrikeEvent.DirectDamageEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.DirectDamageEvent.BaseDamage.Should().Be(495); // 1500 [WeaponDmg] * 0.33 [SkillModifier]
        lungingStrikeEvent.DirectDamageEvent.DamageType.Should().Be(DamageType.Physical);
        lungingStrikeEvent.DirectDamageEvent.DamageSource.Should().Be(DamageSource.LungingStrike);
        lungingStrikeEvent.DirectDamageEvent.SkillType.Should().Be(SkillType.Basic);
        lungingStrikeEvent.DirectDamageEvent.LuckyHitChance.Should().Be(0.5);
        lungingStrikeEvent.DirectDamageEvent.Weapon.Should().Be(skillWeapon);
        lungingStrikeEvent.DirectDamageEvent.Enemy.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void Averages_Weapon_Min_Max_Damage()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike] = new GearItem { MinDamage = 300, MaxDamage = 500, AttacksPerSecond = 1 };
        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(lungingStrikeEvent, _state);

        lungingStrikeEvent.DirectDamageEvent.BaseDamage.Should().Be(132); // 400 [WeaponDmg] * 0.33 [SkillModifier]
    }

    [Fact]
    public void Applies_Skill_Multiplier()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike] = new GearItem { MinDamage = 1, MaxDamage = 1, AttacksPerSecond = 1 };
        _state.Config.Skills.Add(Skill.LungingStrike, 4);
        _mockLungingStrike.Setup(x => x.GetSkillMultiplier(It.Is<SimulationState>(x => x.Config.Skills[Skill.LungingStrike] == 4)))
                          .Returns(0.42);

        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(lungingStrikeEvent, _state);

        lungingStrikeEvent.DirectDamageEvent.BaseDamage.Should().Be(0.42); // 1 [WeaponDmg] * 0.42 [SkillModifier]
    }

    [Fact]
    public void Creates_WeaponCooldownAuraAppliedEvent()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike] = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(lungingStrikeEvent, _state);

        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(lungingStrikeEvent.WeaponCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WeaponCooldown);
        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(1.0);
        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Aura.Should().Be(Aura.WeaponCooldown);
    }

    [Fact]
    public void Considers_Weapon_AttacksPerSecond_When_Creating_WeaponCooldownCompletedEvent()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike] = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 2 };

        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(lungingStrikeEvent, _state);

        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.5);
    }

    [Fact]
    public void Considers_AttackSpeed_Bonuses_When_Creating_WeaponCooldownAuraAppliedEvent()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike] = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };
        _mockAttackSpeedCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>()))
                                  .Returns(0.6);

        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(lungingStrikeEvent, _state);

        lungingStrikeEvent.WeaponCooldownAuraAppliedEvent.Duration.Should().Be(0.6);
    }

    [Fact]
    public void Creates_GenerateFuryEvent()
    {
        _state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike] = new GearItem { MinDamage = 1, MaxDamage = 2, AttacksPerSecond = 1 };

        var lungingStrikeEvent = new LungingStrikeEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(lungingStrikeEvent, _state);

        _state.Events.Should().Contain(lungingStrikeEvent.FuryGeneratedEvent);
        _state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        lungingStrikeEvent.FuryGeneratedEvent.Timestamp.Should().Be(123);
        lungingStrikeEvent.FuryGeneratedEvent.BaseFury.Should().Be(10);
    }
}
