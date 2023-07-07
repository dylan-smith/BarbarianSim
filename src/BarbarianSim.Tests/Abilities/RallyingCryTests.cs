using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class RallyingCryTests
{
    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());

        RallyingCry.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.RallyingCryCooldown);

        RallyingCry.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_RallyingCryEvent()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        RallyingCry.Use(state);

        state.Events.Should().ContainSingle(e => e is RallyingCryEvent);
        state.Events.Cast<RallyingCryEvent>().First().Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 1.0)]
    [InlineData(1, 1.40)]
    [InlineData(2, 1.44)]
    [InlineData(3, 1.48)]
    [InlineData(4, 1.52)]
    [InlineData(5, 1.56)]
    [InlineData(6, 1.56)]
    public void Skill_Points_Determines_ResourceGeneration(int skillPoints, double resourceGeneration)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.RallyingCry, skillPoints);

        RallyingCry.GetResourceGeneration(state).Should().Be(resourceGeneration);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.RallyingCry, 1);
        state.Config.Gear.Helm.RallyingCry = 2;

        RallyingCry.GetResourceGeneration(state).Should().Be(1.48);
    }

    [Fact]
    public void StrategicRallyingCry_Creates_FortifyGeneratedEvent_On_Direct_Damage()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Player.MaxLife = 4000;
        var damageEvent = new DamageEvent(123, 500, DamageType.DirectCrit, DamageSource.Whirlwind, state.Enemies.First());

        RallyingCry.ProcessEvent(damageEvent, state);

        state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        state.Events.OfType<FortifyGeneratedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<FortifyGeneratedEvent>().First().Amount.Should().Be(80);
    }

    [Fact]
    public void StrategicRallyingCry_Does_Not_Fortify_If_Missing_Skill()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Player.MaxLife = 4000;
        var damageEvent = new DamageEvent(123, 500, DamageType.DirectCrit, DamageSource.Whirlwind, state.Enemies.First());

        RallyingCry.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }

    [Fact]
    public void StrategicRallyingCry_Does_Not_Fortify_If_DamageOverTime()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Player.MaxLife = 4000;
        var damageEvent = new DamageEvent(123, 500, DamageType.DamageOverTime, DamageSource.Bleeding, state.Enemies.First());

        RallyingCry.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }

    [Fact]
    public void StrategicRallyingCry_Does_Not_Fortify_If_Shout_Not_Active()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        state.Player.MaxLife = 4000;
        var damageEvent = new DamageEvent(123, 500, DamageType.DirectCrit, DamageSource.Whirlwind, state.Enemies.First());

        RallyingCry.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }
}
