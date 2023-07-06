using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class RallyingCryExpiredEventTests
{
    [Fact]
    public void Removes_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.RallyingCry);
        var e = new RallyingCryExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(Aura.RallyingCry);
    }

    [Fact]
    public void Leaves_RallyingCry_Aura_If_Other_RallyingCryExpiredEvents_Exist()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Events.Add(new RallyingCryExpiredEvent(126.0));
        var e = new RallyingCryExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.RallyingCry);
    }
}
