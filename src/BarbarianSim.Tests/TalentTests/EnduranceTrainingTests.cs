using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class EnduranceTrainingTests
    {
        [TestMethod]
        public void EnduranceTraining()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.EnduranceTraining, 5);

            // 3488 base health + 107 stam + 5% talent
            Assert.AreEqual((3488 + 1070) * 1.05, HealthCalculator.Calculate(state));
            // TODO: Test pet health increase
        }
    }
}
