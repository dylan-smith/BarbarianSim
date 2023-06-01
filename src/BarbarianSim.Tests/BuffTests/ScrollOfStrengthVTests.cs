using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ScrollOfStrengthVTests
    {
        [TestMethod]
        public void ScrollOfStrengthV()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ScrollOfStrengthV);

            Assert.AreEqual(Constants.DRAENEI_STR + 20, StrengthCalculator.Calculate(state));
        }
    }
}
