using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ScrollOfAgilityVTests
    {
        [TestMethod]
        public void ScrollOfAgilityV()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ScrollOfAgilityV);

            Assert.AreEqual(Constants.DRAENEI_AGI + 20, AgilityCalculator.Calculate(state));
        }
    }
}
