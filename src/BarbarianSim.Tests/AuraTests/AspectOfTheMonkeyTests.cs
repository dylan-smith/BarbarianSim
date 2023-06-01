using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.AuraTests
{
    [TestClass]
    public class AspectOfTheMonkeyTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
        }

        [TestMethod]
        public void AspectOfTheMonkey()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Auras.Add(Aura.AspectOfTheMonkey);

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(0));

            Assert.AreEqual(0.08, DodgeCalculator.Calculate(state));
        }
    }
}
