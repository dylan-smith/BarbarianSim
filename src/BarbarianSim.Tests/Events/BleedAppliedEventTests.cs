using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class BleedAppliedEventTests
{
    [Fact]
    public void Adds_Bleeding_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BleedAppliedEvent(123.0, 500.0, 5.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Bleeding);
    }

    [Fact]
    public void Creates_BleedCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BleedAppliedEvent(123.0, 500.0, 5.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Events.Should().Contain(e.BleedCompletedEvent);
        state.Events.Should().ContainSingle(e => e is BleedCompletedEvent);
        e.BleedCompletedEvent.Timestamp.Should().Be(128.0);
        e.BleedCompletedEvent.Damage.Should().Be(500.0);
    }
}
