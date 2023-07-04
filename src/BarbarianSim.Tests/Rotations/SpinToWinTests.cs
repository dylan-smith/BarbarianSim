using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Rotations;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Rotations;

public class SpinToWinTests
{
    [Fact]
    public void Uses_LungingStrike_When_Available()
    {
        var state = new SimulationState(new SimulationConfig());
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Events.Any(e => e is LungingStrikeEvent).Should().BeTrue();
    }

    [Fact]
    public void Does_Nothing_When_Lunging_Strike_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WeaponCooldown);
        var rotation = new SpinToWin();

        rotation.Execute(state);

        state.Events.Should().BeEmpty();
    }
}
