using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class CatlikeReflexesTests
    {
        // TODO: test the additional pet dodge

        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
        }

        [TestMethod]
        public void CatlikeReflexes()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.CatlikeReflexes, 3);

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(0.0));

            Assert.AreEqual(0.03, DodgeCalculator.Calculate(state), 0.00001);
        }
    }
}
