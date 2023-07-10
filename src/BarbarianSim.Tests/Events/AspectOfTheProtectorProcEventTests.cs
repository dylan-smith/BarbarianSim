using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class AspectOfTheProtectorProcEventTests
{
    [Fact]
    public void Adds_AspectOfTheProtectorCooldown_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new AspectOfTheProtectorProcEvent(123.0, 1000);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.AspectOfTheProtectorCooldown);
    }

    [Fact]
    public void Creates_AspectOfTheProtectorCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new AspectOfTheProtectorProcEvent(123.0, 1000);

        e.ProcessEvent(state);

        state.Events.Should().Contain(e.AspectOfTheProtectorCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        e.AspectOfTheProtectorCooldownCompletedEvent.Timestamp.Should().Be(153.0);
        e.AspectOfTheProtectorCooldownCompletedEvent.Aura.Should().Be(Aura.AspectOfTheProtectorCooldown);
    }

    [Fact]
    public void Creates_BarrierAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new AspectOfTheProtectorProcEvent(123.0, 1000);

        e.ProcessEvent(state);

        state.Events.Should().Contain(e.BarrierAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BarrierAppliedEvent);
        e.BarrierAppliedEvent.Timestamp.Should().Be(123.0);
        e.BarrierAppliedEvent.BarrierAmount.Should().Be(1000);
        e.BarrierAppliedEvent.Duration.Should().Be(10.0);
    }
}
