using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.Buffs
{
    [TestClass]
    public class GraceOfAirTotemTests
    {
        [TestMethod]
        public void GraceOfAirTotem()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.GraceOfAirTotem);

            Assert.AreEqual(Constants.DRAENEI_AGI + 77, AgilityCalculator.Calculate(state));
        }
    }
}
