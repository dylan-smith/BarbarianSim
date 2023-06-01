using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class LethalShotsTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
        }

        [TestMethod]
        public void LethalShots()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;
            state.Config.Talents.Add(Talent.LethalShots, 5);

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT));

            Assert.AreEqual(0.05, RangedCritCalculator.Calculate(state), 0.001);
        }
    }
}
