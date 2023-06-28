using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class FocusedFireTests
    {
        [TestMethod]
        public void FocusedFire()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.FocusedFire, 2);

            Assert.AreEqual(1.02, DamageMultiplierCalculator.Calculate(state), 0.00001);
        }

        // TODO: test for the increased crit on Kill Command once we have Kill Command implemented
    }
}
