using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class ChallengingShoutCooldownCompletedEventTests
{
    [Fact]
    public void Removes_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ChallengingShoutCooldown);
        var e = new ChallengingShoutCooldownCompletedEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(Aura.ChallengingShoutCooldown);
    }

    [Fact]
    public void Throws_If_Aura_Is_Missing()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new ChallengingShoutCooldownCompletedEvent(0.0);

        var act = () => e.ProcessEvent(state);

        act.Should().Throw<Exception>();
    }
}
