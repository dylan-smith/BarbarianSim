using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class LightningReflexesTests
    {
        [TestMethod]
        public void LightningReflexes()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.LightningReflexes, 5);

            // 148 * 1.15
            Assert.AreEqual(170, AgilityCalculator.Calculate(state), 0.001);
        }
    }
}
