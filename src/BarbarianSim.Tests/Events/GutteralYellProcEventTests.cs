using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class GutteralYellProcEventTests
{
    [Fact]
    public void Adds_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var gutteralYellProcEvent = new GutteralYellProcEvent(123);

        gutteralYellProcEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.GutteralYell);
    }

    [Fact]
    public void Creates_AuraExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var gutteralYellProcEvent = new GutteralYellProcEvent(123);

        gutteralYellProcEvent.ProcessEvent(state);

        gutteralYellProcEvent.GutteralYellExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(gutteralYellProcEvent.GutteralYellExpiredEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        gutteralYellProcEvent.GutteralYellExpiredEvent.Timestamp.Should().Be(128);
    }
}
