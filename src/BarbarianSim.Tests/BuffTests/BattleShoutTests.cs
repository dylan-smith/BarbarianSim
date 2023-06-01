using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class BattleShoutTests
    {
        [TestMethod]
        public void BattleShout()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.BattleShout);

            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 305, MeleeAttackPowerCalculator.Calculate(state));
        }
    }
}
