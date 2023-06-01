using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class KillerInstinctTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
        }

        [TestMethod]
        public void KillerInstinct()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT));

            state.Config.Talents.Add(Talent.KillerInstinct, 3);

            Assert.AreEqual(0.03, MeleeCritCalculator.Calculate(state), 0.0001);
            Assert.AreEqual(0.03, RangedCritCalculator.Calculate(state), 0.0001);
            Assert.AreEqual(0.03, SpellCritCalculator.Calculate(state), 0.001);
            // TODO: does this affect pet crit also??
        }
    }
}
