using BarbarianSim.Config;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class FortifyGeneratedEventTests
{
    [Fact]
    public void Adds_Fortify_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new FortifyGeneratedEvent(123.0, 12.0);

        e.ProcessEvent(state);

        state.Player.Fortify.Should().Be(12.0);
    }

    [Fact]
    public void Caps_Fortify_At_Max_Life()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.MaxLife = 4000;
        state.Player.Fortify = 3970;
        var e = new FortifyGeneratedEvent(123.0, 80.0);

        e.ProcessEvent(state);

        state.Player.Fortify.Should().Be(4000);
    }
}
