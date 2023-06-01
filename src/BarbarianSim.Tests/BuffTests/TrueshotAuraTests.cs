using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class TrueshotAuraTests
    {
        [TestMethod]
        public void TrueShotAura()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.TrueshotAura);

            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 125, MeleeAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 125, RangedAttackPowerCalculator.Calculate(state));
        }

        [TestMethod]
        public void TrueShotAuraFromTalent()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.TrueshotAura, 1);

            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 125, MeleeAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 125, RangedAttackPowerCalculator.Calculate(state));
        }

        [TestMethod]
        public void TrueShotAuraFromTalentAndBuff()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.TrueshotAura, 1);
            state.Config.Buffs.Add(Buff.TrueshotAura);

            Assert.AreEqual(Constants.DRAENEI_STR + Constants.DRAENEI_AGI + Constants.BASE_MAP + 125, MeleeAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 125, RangedAttackPowerCalculator.Calculate(state));
        }
    }
}
