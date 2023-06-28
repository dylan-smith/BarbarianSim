using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class SurvivalInstinctsTests
    {
        [TestMethod]
        public void SurvivalInstincts()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.SurvivalInstincts, 2);

            Assert.AreEqual(((Constants.BASE_RAP + Constants.DRAENEI_AGI) * 1.04).Floor(), RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(((Constants.BASE_MAP + Constants.DRAENEI_STR + Constants.DRAENEI_AGI) * 1.04).Floor(), MeleeAttackPowerCalculator.Calculate(state));
        }
    }
}
