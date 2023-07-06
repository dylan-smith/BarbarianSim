using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class BerserkingAppliedEventTests
{
    [Fact]
    public void Adds_Berserking_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BerserkingAppliedEvent(123.0, 1.5);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Berserking);
    }

    [Fact]
    public void Creates_BerserkingExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BerserkingAppliedEvent(123.0, 1.5);

        e.ProcessEvent(state);

        state.Events.Should().Contain(e.BerserkingExpiredEvent);
        state.Events.Should().ContainSingle(e => e is BerserkingExpiredEvent);
        e.BerserkingExpiredEvent.Timestamp.Should().Be(124.5);
    }
}
