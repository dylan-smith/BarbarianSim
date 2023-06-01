using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class GrilledMudfishTests
    {
        [TestMethod]
        public void GrilledMudfish()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.GrilledMudfish);

            Assert.AreEqual(Constants.DRAENEI_AGI + 20, AgilityCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_SPI + 20, SpiritCalculator.Calculate(state));
        }
    }
}
