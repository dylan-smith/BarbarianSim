using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class ViolentWhirlwindAppliedEventTests
{
    [Fact]
    public void Adds_ViolentWhirlwind_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new ViolentWhirlwindAppliedEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.ViolentWhirlwind);
    }
}
