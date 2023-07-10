using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class GhostwalkerAspectTests
{
    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspect = new GhostwalkerAspect(17);

        var unstoppableAppliedEvent = new AuraAppliedEvent(123, 5, Aura.Unstoppable);

        aspect.ProcessEvent(unstoppableAppliedEvent, state);

        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(9);
        state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Ghostwalker);
    }
}
