using BarbarianSim.Config;
using BarbarianSim.Enums;
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

        var barrierExpiredEvent = new BarrierExpiredEvent(123.0, barrier);

        _handler.ProcessEvent(barrierExpiredEvent, _state);

        _state.Player.Barriers.Should().BeEmpty();
        _state.Player.Auras.Should().BeEmpty();
    }

    [Fact]
    public void Leaves_Aura_If_Extra_Barriers_Remain()
    {
        _state.Player.Auras.Add(Aura.Barrier);
        var barrier1 = new Barrier(1000);
        var barrier2 = new Barrier(2000);
        _state.Player.Barriers.Add(barrier1);
        _state.Player.Barriers.Add(barrier2);

        var barrierExpiredEvent = new BarrierExpiredEvent(123.0, barrier1);

        _handler.ProcessEvent(barrierExpiredEvent, _state);

        _state.Player.Auras.Should().Contain(Aura.Barrier);
    }
}
