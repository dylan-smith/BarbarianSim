using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ImprovedHuntersMarkTests
    {
        [TestMethod]
        public void ImprovedHuntersMark()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ImprovedHuntersMark);

            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 440, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 110, MeleeAttackPowerCalculator.Calculate(state));
        }

        [TestMethod]
        public void ImprovedHuntersMarkBuffAndTalent()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ImprovedHuntersMark);
            state.Config.Talents.Add(Talent.ImprovedHuntersMark, 5);

            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 440, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 110, MeleeAttackPowerCalculator.Calculate(state));
        }
    }
}
