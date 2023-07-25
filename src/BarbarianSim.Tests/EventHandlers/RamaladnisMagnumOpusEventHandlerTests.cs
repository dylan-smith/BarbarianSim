using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class RamaladnisMagnumOpusEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly RamaladnisMagnumOpusEventHandler _handler = new();

    [Fact]
    public void Creates_FurySpentEvent()
    {
        var ramaladnisMagnumOpusEvent = new RamaladnisMagnumOpusEvent(123.0);

        _handler.ProcessEvent(ramaladnisMagnumOpusEvent, _state);

        ramaladnisMagnumOpusEvent.FurySpentEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ramaladnisMagnumOpusEvent.FurySpentEvent);
        ramaladnisMagnumOpusEvent.FurySpentEvent.Timestamp.Should().Be(123);
        ramaladnisMagnumOpusEvent.FurySpentEvent.BaseFurySpent.Should().Be(2);
        ramaladnisMagnumOpusEvent.FurySpentEvent.SkillType.Should().Be(SkillType.None);
    }

    [Fact]
    public void Creates_RamaladnisMagnumOpusEvent()
    {
        var ramaladnisMagnumOpusEvent = new RamaladnisMagnumOpusEvent(123.0);

        _handler.ProcessEvent(ramaladnisMagnumOpusEvent, _state);

        ramaladnisMagnumOpusEvent.NextRamaladnisMagnumOpusEvent.Should().NotBeNull();
        _state.Events.Should().Contain(ramaladnisMagnumOpusEvent.NextRamaladnisMagnumOpusEvent);
        ramaladnisMagnumOpusEvent.NextRamaladnisMagnumOpusEvent.Timestamp.Should().Be(124);
    }
}
