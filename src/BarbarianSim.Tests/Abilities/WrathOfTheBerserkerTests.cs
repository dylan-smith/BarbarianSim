using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class WrathOfTheBerserkerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WrathOfTheBerserker _wrathOfTheBerserker = new();

    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        _wrathOfTheBerserker.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        _state.Player.Auras.Add(Aura.WrathOfTheBerserkerCooldown);

        _wrathOfTheBerserker.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_Returns_False_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 0);
        _wrathOfTheBerserker.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_WrathOfTheBerserkerEvent()
    {
        _state.CurrentTime = 123;

        _wrathOfTheBerserker.Use(_state);

        _state.Events.Should().ContainSingle(e => e is WrathOfTheBerserkerEvent);
        _state.Events.OfType<WrathOfTheBerserkerEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_BerserkingAppliedEvent_On_Direct_Damage_With_Basic_Skills()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct | DamageType.CriticalStrike, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(5);
    }

    [Fact]
    public void Does_Not_Berserk_If_Missing_Skill()
    {
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void Does_Not_Berserk_If_Core_Skill()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Core, 0, null, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void Does_Not_Berserk_If_Aura_Missing()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, 0, null, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }
}
