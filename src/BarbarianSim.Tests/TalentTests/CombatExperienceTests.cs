using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class CombatExperienceTests
    {
        [TestMethod]
        public void CombatExperience()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.CombatExperience, 2);

            // 148 * 1.02
            Assert.AreEqual(150, AgilityCalculator.Calculate(state));
            // 78 * 1.06
            Assert.AreEqual(82, IntellectCalculator.Calculate(state));
        }
    }
}
