using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class LeaderOfThePackTests
    {
        [TestMethod]
        public void LeaderOfThePack()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;
            state.Config.Buffs.Add(Buff.LeaderOfThePack);

            // base crit is -1.53% and there's a 4.8% crit suppression on raid bosses
            var baseCrit = -0.0153 - 0.048;
            // 40 agi per 1% crit
            baseCrit += Constants.DRAENEI_AGI / 4000.0;

            Assert.AreEqual(baseCrit + 0.05, MeleeCritCalculator.Calculate(state), 0.0001);
            Assert.AreEqual(baseCrit + 0.05, RangedCritCalculator.Calculate(state), 0.0001);
        }
    }
}
