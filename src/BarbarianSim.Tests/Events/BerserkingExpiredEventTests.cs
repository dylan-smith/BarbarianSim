using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class BerserkingExpiredEventTests
{
    [Fact]
    public void Removes_Berserking_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Berserking);
        var e = new BerserkingExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(Aura.Berserking);
    }

    [Fact]
    public void Leaves_Berserking_Aura_If_Other_BerserkingExpiredEvents_Exist()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.Berserking);
        state.Events.Add(new BerserkingExpiredEvent(126.0));
        var e = new BerserkingExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Berserking);
    }
}
