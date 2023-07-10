using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class AspectOfTheProtectorProcEventTests
{
    [Fact]
    public void Creates_AspectOfTheProtectorCooldownAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new AspectOfTheProtectorProcEvent(123.0, 1000);

        e.ProcessEvent(state);

        state.Events.Should().Contain(e.AspectOfTheProtectorCooldownAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        e.AspectOfTheProtectorCooldownAuraAppliedEvent.Timestamp.Should().Be(123.0);
        e.AspectOfTheProtectorCooldownAuraAppliedEvent.Duration.Should().Be(30.0);
        e.AspectOfTheProtectorCooldownAuraAppliedEvent.Aura.Should().Be(Aura.AspectOfTheProtectorCooldown);
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
