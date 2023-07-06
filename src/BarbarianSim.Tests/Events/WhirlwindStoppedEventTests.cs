using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WhirlwindStoppedEventTests
{
    [Fact]
    public void Removes_Whirlwinding_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Whirlwinding);

        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(0);
        whirlwindStoppedEvent.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(Aura.Whirlwinding);
    }

    [Fact]
    public void Removes_ViolentWhirlwind_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ViolentWhirlwind);

        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(0);
        whirlwindStoppedEvent.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(Aura.ViolentWhirlwind);
    }
}
