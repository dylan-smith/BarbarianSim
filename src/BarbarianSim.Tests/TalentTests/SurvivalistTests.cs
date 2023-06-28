using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class SurvivalistTests
    {
        [TestMethod]
        public void Survivalist()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.Survivalist, 5);

            // Base Health + Stamina * 10 * 1.1
            Assert.AreEqual((3488 + 1070) * 1.1, HealthCalculator.Calculate(state));
        }
    }
}
