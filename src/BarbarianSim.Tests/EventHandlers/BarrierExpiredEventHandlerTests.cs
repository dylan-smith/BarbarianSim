using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class BarrierExpiredEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly BarrierExpiredEventHandler _handler = new();

    [Fact]
    public void Removes_Barrier_From_Player()
    {
        var barrier = new Barrier(1000);
        _state.Player.Barriers.Add(barrier);

        var barrierExpiredEvent = new BarrierExpiredEvent(123.0, null, barrier);

        _handler.ProcessEvent(barrierExpiredEvent, _state);

        _state.Player.Barriers.Should().BeEmpty();
        _state.Player.Auras.Should().BeEmpty();
    }
}
