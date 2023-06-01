using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ImprovedBlessingOfMightTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
            RandomGenerator.ClearMock();
        }

        [TestMethod]
        public void ImprovedBlessingOfMight()
        {
            var state = new SimulationState();
            state.Config.Buffs.Add(Buff.ImprovedBlessingOfMight);

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(0.0));
            BaseStatCalculator.InjectMock(typeof(StrengthCalculator), new FakeStatCalculator(0.0));

            Assert.AreEqual(Constants.BASE_RAP + 264, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.BASE_MAP + 264, MeleeAttackPowerCalculator.Calculate(state));
        }
    }
}
