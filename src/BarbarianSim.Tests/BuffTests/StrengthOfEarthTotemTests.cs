using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class StrengthOfEarthTotemTests
    {
        [TestMethod]
        public void StrengthOfEarthTotem()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.StrengthOfEarthTotem);

            Assert.AreEqual(Constants.DRAENEI_STR + 86, StrengthCalculator.Calculate(state));
        }
    }
}
