using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.AuraTests
{
    [TestClass]
    public class ImprovedAspectOfTheHawkTests
    {
        [TestMethod]
        public void ImprovedAspectOfTheHawk()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.ImprovedAspectOfTheHawk, 5);
            state.Auras.Add(Aura.ImprovedAspectOfTheHawk);

            Assert.AreEqual(1.15, RangedHasteCalculator.Calculate(state), 0.0001);
        }
    }
}
