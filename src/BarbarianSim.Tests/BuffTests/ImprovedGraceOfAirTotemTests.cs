using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ImprovedGraceOfAirTotemTests
    {
        [TestMethod]
        public void ImprovedGraceOfAirTotem()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ImprovedGraceOfAirTotem);

            Assert.AreEqual(Constants.DRAENEI_AGI + 88, AgilityCalculator.Calculate(state));
        }
    }
}
