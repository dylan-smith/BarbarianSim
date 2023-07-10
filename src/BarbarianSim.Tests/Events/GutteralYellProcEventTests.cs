using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class GutteralYellProcEventTests
{
    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var gutteralYellProcEvent = new GutteralYellProcEvent(123);

        gutteralYellProcEvent.ProcessEvent(state);

        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(gutteralYellProcEvent.GutteralYellAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Timestamp.Should().Be(123);
        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Duration.Should().Be(5);
        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Aura.Should().Be(Aura.GutteralYell);
    }
}
