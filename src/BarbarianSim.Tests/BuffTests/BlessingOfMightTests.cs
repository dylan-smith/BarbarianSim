using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class BlessingOfMightTests
    {
        [TestMethod]
        public void BlessingOfMight()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.BlessingOfMight);

            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 220, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 220, MeleeAttackPowerCalculator.Calculate(state));
        }
    }
}
