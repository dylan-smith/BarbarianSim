using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class BleedAppliedEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly BleedAppliedEventHandler _handler = new();

    [Fact]
    public void Adds_Bleeding_Aura()
    {
        var bleedAppliedEvent = new BleedAppliedEvent(123.0, 500.0, 5.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedAppliedEvent, _state);

        _state.Enemies.First().Auras.Should().Contain(Aura.Bleeding);
    }

    [Fact]
    public void Creates_BleedCompletedEvent()
    {
        var bleedAppliedEvent = new BleedAppliedEvent(123.0, 500.0, 5.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedAppliedEvent, _state);

        _state.Events.Should().Contain(bleedAppliedEvent.BleedCompletedEvent);
        _state.Events.Should().ContainSingle(e => e is BleedCompletedEvent);
        bleedAppliedEvent.BleedCompletedEvent.Timestamp.Should().Be(128.0);
        bleedAppliedEvent.BleedCompletedEvent.Damage.Should().Be(500.0);
        bleedAppliedEvent.BleedCompletedEvent.Target.Should().Be(_state.Enemies.First());
    }
}
