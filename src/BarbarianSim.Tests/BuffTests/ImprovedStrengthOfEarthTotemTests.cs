using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ImprovedStrengthOfEarthTotemTests
    {
        [TestMethod]
        public void ImprovedStrengthOfEarthTotem()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ImprovedStrengthOfEarthTotem);

            Assert.AreEqual(Constants.DRAENEI_STR + 98, StrengthCalculator.Calculate(state));
        }
    }
}
