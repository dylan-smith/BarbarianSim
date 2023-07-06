using BarbarianSim.Config;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class PressurePointProcEventTests
{
    [Fact]
    public void Creates_VulnerableAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var pressurePointProcEvent = new PressurePointProcEvent(123, state.Enemies.First());

        pressurePointProcEvent.ProcessEvent(state);

        pressurePointProcEvent.VulnerableAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(pressurePointProcEvent.VulnerableAppliedEvent);
        state.Events.Should().ContainSingle(e => e is VulnerableAppliedEvent);
        pressurePointProcEvent.VulnerableAppliedEvent.Timestamp.Should().Be(123);
        pressurePointProcEvent.VulnerableAppliedEvent.Target.Should().Be(state.Enemies.First());
        pressurePointProcEvent.VulnerableAppliedEvent.Duration.Should().Be(2.0);
    }
}
