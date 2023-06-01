using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class RangedWeaponSpecializationTests
    {
        [TestMethod]
        public void RangedWeaponSpecialization()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.RangedWeaponSpecialization, 5);

            Assert.AreEqual(1.05, RangedDamageMultiplierCalculator.Calculate(state), 0.00001);
        }
    }
}
