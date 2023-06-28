using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class HumanoidSlayingTests
    {
        [TestMethod]
        public void HumanoidSlaying()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.BossSettings.BossType = BossType.Humanoid;
            state.Config.Talents.Add(Talent.HumanoidSlaying, 3);

            Assert.AreEqual(1.03, DamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, RangedCritDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, MeleeCritDamageMultiplierCalculator.Calculate(state));
        }
    }
}
