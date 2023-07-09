using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class WrathOfTheBerserkerTests
{
    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());

        WrathOfTheBerserker.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WrathOfTheBerserkerCooldown);

        WrathOfTheBerserker.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_WrathOfTheBerserkerEvent()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        WrathOfTheBerserker.Use(state);

        state.Events.Should().ContainSingle(e => e is WrathOfTheBerserkerEvent);
        state.Events.OfType<WrathOfTheBerserkerEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_BerserkingAppliedEvent_On_Direct_Damage_With_Basic_Skills()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.DirectCrit, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().ContainSingle(e => e is BerserkingAppliedEvent);
        state.Events.OfType<BerserkingAppliedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<BerserkingAppliedEvent>().First().Duration.Should().Be(5);
    }

    [Fact]
    public void Does_Not_Berserk_If_Missing_Skill()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.DirectCrit, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is BerserkingAppliedEvent);
    }

    [Fact]
    public void Does_Not_Berserk_If_Core_Skill()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.DirectCrit, DamageSource.LungingStrike, SkillType.Core, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is BerserkingAppliedEvent);
    }

    [Fact]
    public void Does_Not_Berserk_If_Bleeding_Damage()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is BerserkingAppliedEvent);
    }

    [Fact]
    public void Does_Not_Berserk_If_Aura_Missing()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        var damageEvent = new DamageEvent(123, 500, DamageType.DirectCrit, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is BerserkingAppliedEvent);
    }
}
