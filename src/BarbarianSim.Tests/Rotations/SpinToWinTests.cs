using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.Rotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.Rotations;

[TestClass]
public class SpinToWinTests
{
    [TestMethod]
    public void Uses_LungingStrike_When_Available()
    {
        var state = new SimulationState(new SimulationConfig());
        var rotation = new SpinToWin();

        rotation.Execute(state);

        Assert.IsTrue(state.Events.Any(e => e is LungingStrikeEvent));
    }

    [TestMethod]
    public void Does_Nothing_When_Lunging_Strike_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Auras.Add(Aura.WeaponCooldown);
        var rotation = new SpinToWin();

        rotation.Execute(state);

        Assert.AreEqual(0, state.Events.Count);
    }
}
