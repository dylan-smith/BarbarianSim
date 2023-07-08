using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class GutteralYellTests
{
    [Fact]
    public void Creates_ProcEvent_On_WarCry()
    {
        var state = new SimulationState(new SimulationConfig());
        var shoutEvent = new WarCryEvent(123);

        GutteralYell.ProcessEvent(shoutEvent, state);

        state.Events.Should().ContainSingle(e => e is GutteralYellProcEvent);
        state.Events.OfType<GutteralYellProcEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_ProcEvent_On_ChallengingShout()
    {
        var state = new SimulationState(new SimulationConfig());
        var shoutEvent = new ChallengingShoutEvent(123);

        GutteralYell.ProcessEvent(shoutEvent, state);

        state.Events.Should().ContainSingle(e => e is GutteralYellProcEvent);
        state.Events.OfType<GutteralYellProcEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_ProcEvent_On_RallyingCry()
    {
        var state = new SimulationState(new SimulationConfig());
        var shoutEvent = new RallyingCryEvent(123);

        GutteralYell.ProcessEvent(shoutEvent, state);

        state.Events.Should().ContainSingle(e => e is GutteralYellProcEvent);
        state.Events.OfType<GutteralYellProcEvent>().First().Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 4)]
    [InlineData(2, 8)]
    [InlineData(3, 12)]
    [InlineData(4, 12)]
    public void Skill_Points_Determines_Percent_Proc(int skillPoints, double damageReduction)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.GutteralYell, skillPoints);

        GutteralYell.GetDamageReduction(state).Should().Be(damageReduction);
    }
}
