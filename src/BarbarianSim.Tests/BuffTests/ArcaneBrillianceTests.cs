using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ArcaneBrillianceTests
    {
        [TestMethod]
        public void ArcaneBrilliance()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ArcaneBrilliance);

            Assert.AreEqual(Constants.DRAENEI_INT + 40, IntellectCalculator.Calculate(state));
        }
    }
}
