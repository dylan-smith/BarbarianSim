using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ElixirOfMajorAgilityTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
            RandomGenerator.ClearMock();
        }

        [TestMethod]
        public void ElixirOfMajorAgility()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;
            state.Config.Buffs.Add(Buff.ElixirOfMajorAgility);

            Assert.AreEqual(Constants.DRAENEI_AGI + 35, AgilityCalculator.Calculate(state));

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT));

            Assert.AreEqual(20.0 / 2208, MeleeCritCalculator.Calculate(state), 0.00001);
            Assert.AreEqual(20.0 / 2208, RangedCritCalculator.Calculate(state), 0.00001);
        }
    }
}
