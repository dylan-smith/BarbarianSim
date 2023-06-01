using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class EnhancedBattleShoutTests
    {
        [TestMethod]
        public void EnhancedBattleShout()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ImprovedBattleShout);

            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 381, MeleeAttackPowerCalculator.Calculate(state));
        }
    }
}
