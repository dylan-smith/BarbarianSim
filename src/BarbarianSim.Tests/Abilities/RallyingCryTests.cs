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
}
