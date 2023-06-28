using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class ImprovedAspectOfTheMonkeyTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
        }

        [TestMethod]
        public void ImprovedAspectOfTheMonkey()
        {
            var state = new SimulationState();
            state.Auras.Add(Aura.AspectOfTheMonkey);
            state.Config.Talents.Add(Talent.ImprovedAspectOfTheMonkey, 3);

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(0.0));

            Assert.AreEqual(0.08 + 0.06, DodgeCalculator.Calculate(state), 0.00001);
        }
    }
}
