using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class BarrierExpiredEventTests
{
    [Fact]
    public void Removes_Barrier_From_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var barrier = new Barrier(1000);
        state.Player.Barriers.Add(barrier);

        var e = new BarrierExpiredEvent(123.0, barrier);

        e.ProcessEvent(state);

        state.Player.Barriers.Should().BeEmpty();
        state.Player.Auras.Should().BeEmpty();
    }

    [Fact]
    public void Leaves_Aura_If_Extra_Barriers_Remain()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Barrier);
        var barrier1 = new Barrier(1000);
        var barrier2 = new Barrier(2000);
        state.Player.Barriers.Add(barrier1);
        state.Player.Barriers.Add(barrier2);

        var e = new BarrierExpiredEvent(123.0, barrier1);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Barrier);
    }
}
