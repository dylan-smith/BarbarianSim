using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class CooldownCompletedEventTests
{
    [Fact]
    public void Removes_Aura()
    {
        var testAura = Aura.WarCry;

        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(testAura);
        var e = new CooldownCompletedEvent(123.0, testAura);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(testAura);
    }

    [Fact]
    public void Throws_If_Aura_Is_Missing()
    {
        var testAura = Aura.WarCry;

        var state = new SimulationState(new SimulationConfig());
        var e = new CooldownCompletedEvent(123.0, testAura);

        var act = () => e.ProcessEvent(state);

        act.Should().Throw<Exception>();
    }
}
