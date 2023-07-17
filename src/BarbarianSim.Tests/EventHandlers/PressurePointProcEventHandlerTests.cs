using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class PressurePointProcEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly PressurePointProcEventHandler _handler = new();

    [Fact]
    public void Creates_VulnerableAppliedEvent()
    {
        var pressurePointProcEvent = new PressurePointProcEvent(123, _state.Enemies.First());

        _handler.ProcessEvent(pressurePointProcEvent, _state);

        pressurePointProcEvent.VulnerableAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(pressurePointProcEvent.VulnerableAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        pressurePointProcEvent.VulnerableAppliedEvent.Timestamp.Should().Be(123);
        pressurePointProcEvent.VulnerableAppliedEvent.Target.Should().Be(_state.Enemies.First());
        pressurePointProcEvent.VulnerableAppliedEvent.Duration.Should().Be(2.0);
        pressurePointProcEvent.VulnerableAppliedEvent.Aura.Should().Be(Aura.Vulnerable);
    }
}
