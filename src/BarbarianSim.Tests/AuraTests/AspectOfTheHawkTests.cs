using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.AuraTests
{
    [TestClass]
    public class AspectOfTheHawkTests
    {
        [TestMethod]
        public void AspectOfTheHawk()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Auras.Add(Aura.AspectOfTheHawk);

            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 155, RangedAttackPowerCalculator.Calculate(state));
        }
    }
}
