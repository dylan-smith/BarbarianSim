using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class UnstoppableExpiredEventTests
{
    [Fact]
    public void Removes_Unstoppable_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Unstoppable);
        var e = new UnstoppableExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(Aura.Unstoppable);
    }

    [Fact]
    public void Leaves_Unstoppable_Aura_If_Other_UnstoppableExpiredEvents_Exist()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Unstoppable);
        state.Events.Add(new UnstoppableExpiredEvent(126.0));
        var e = new UnstoppableExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Unstoppable);
    }
}
