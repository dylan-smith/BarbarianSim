using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class GhostwalkerAspectTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly GhostwalkerAspect _aspect = new();

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        _aspect.Speed = 17;

        var unstoppableAppliedEvent = new AuraAppliedEvent(123, 5, Aura.Unstoppable);

        _aspect.ProcessEvent(unstoppableAppliedEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(9);
        _state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Ghostwalker);
    }
}
