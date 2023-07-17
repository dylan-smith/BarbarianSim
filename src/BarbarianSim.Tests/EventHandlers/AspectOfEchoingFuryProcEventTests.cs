using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using System;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class AspectOfEchoingFuryProcEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfEchoingFuryProcEventHandler _handler = new();

    [Fact]
    public void Creates_FuryGeneratedEvents_For_Each_Second()
    {
        var aspectProcEvent = new AspectOfEchoingFuryProcEvent(123.0, 6.0, 4);

        _handler.ProcessEvent(aspectProcEvent, _state);

        aspectProcEvent.FuryGeneratedEvents.Should().HaveCount(6);
        _state.Events.OfType<FuryGeneratedEvent>().Should().HaveCount(6);
        aspectProcEvent.FuryGeneratedEvents.First().Timestamp.Should().Be(124);
        aspectProcEvent.FuryGeneratedEvents.Last().Timestamp.Should().Be(129);
        _state.Events.OfType<FuryGeneratedEvent>().All(e => e.BaseFury == 4.0).Should().BeTrue();
        _state.Events.OfType<FuryGeneratedEvent>().All(e => Math.Abs(e.BaseFury - 4) < 0.000001).Should().BeTrue();
    }

    [Fact]
    public void Rounds_Down_Duration()
    {
        var aspectProcEvent = new AspectOfEchoingFuryProcEvent(123.0, 3.96, 4);

        _handler.ProcessEvent(aspectProcEvent, _state);

        aspectProcEvent.FuryGeneratedEvents.Should().HaveCount(3);
    }
}
