using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class CarefulAimTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
        }

        [TestMethod]
        public void CarefulAim()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.CarefulAim, 3);

            BaseStatCalculator.InjectMock(typeof(IntellectCalculator), new FakeStatCalculator(100.0));
            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(0.0));

            Assert.AreEqual(Constants.BASE_RAP + 45, RangedAttackPowerCalculator.Calculate(state));
        }
    }
}
