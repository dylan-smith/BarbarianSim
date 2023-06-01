using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class MonsterSlayingTests
    {
        [TestMethod]
        public void MonsterSlayingBeast()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.BossSettings.BossType = BossType.Beast;
            state.Config.Talents.Add(Talent.MonsterSlaying, 3);

            Assert.AreEqual(1.03, DamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, RangedCritDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, MeleeCritDamageMultiplierCalculator.Calculate(state));
        }

        [TestMethod]
        public void MonsterSlayingGiant()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.BossSettings.BossType = BossType.Giant;
            state.Config.Talents.Add(Talent.MonsterSlaying, 3);

            Assert.AreEqual(1.03, DamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, RangedCritDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, MeleeCritDamageMultiplierCalculator.Calculate(state));
        }

        [TestMethod]
        public void MonsterSlayingDragonkin()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.BossSettings.BossType = BossType.Dragonkin;
            state.Config.Talents.Add(Talent.MonsterSlaying, 3);

            Assert.AreEqual(1.03, DamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, RangedCritDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, MeleeCritDamageMultiplierCalculator.Calculate(state));
        }
    }
}
