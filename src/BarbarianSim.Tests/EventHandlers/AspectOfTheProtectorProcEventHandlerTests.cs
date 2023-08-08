using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class AspectOfTheProtectorProcEventHandlerTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfTheProtectorProcEventHandler _handler;

    public AspectOfTheProtectorProcEventHandlerTests() => _handler = new(_mockSimLogger.Object);

    [Fact]
    public void Creates_AspectOfTheProtectorCooldownAuraAppliedEvent()
    {
        var aspectOfTheProtectorProcEvent = new AspectOfTheProtectorProcEvent(123.0, 1000);

        _handler.ProcessEvent(aspectOfTheProtectorProcEvent, _state);

        _state.Events.Should().Contain(aspectOfTheProtectorProcEvent.AspectOfTheProtectorCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        aspectOfTheProtectorProcEvent.AspectOfTheProtectorCooldownAuraAppliedEvent.Timestamp.Should().Be(123.0);
        aspectOfTheProtectorProcEvent.AspectOfTheProtectorCooldownAuraAppliedEvent.Duration.Should().Be(30.0);
        aspectOfTheProtectorProcEvent.AspectOfTheProtectorCooldownAuraAppliedEvent.Aura.Should().Be(Aura.AspectOfTheProtectorCooldown);
    }

    [Fact]
    public void Creates_BarrierAppliedEvent()
    {
        var aspectOfTheProtectorProcEvent = new AspectOfTheProtectorProcEvent(123.0, 1000);

        _handler.ProcessEvent(aspectOfTheProtectorProcEvent, _state);

        _state.Events.Should().Contain(aspectOfTheProtectorProcEvent.BarrierAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is BarrierAppliedEvent);
        aspectOfTheProtectorProcEvent.BarrierAppliedEvent.Timestamp.Should().Be(123.0);
        aspectOfTheProtectorProcEvent.BarrierAppliedEvent.BarrierAmount.Should().Be(1000);
        aspectOfTheProtectorProcEvent.BarrierAppliedEvent.Duration.Should().Be(10.0);
    }
}
