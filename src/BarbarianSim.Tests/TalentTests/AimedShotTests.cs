using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class AimedShotTests
    {
        // TODO: test the ability/rotation

        [TestMethod]
        public void AimedShotDamageIncrease()
        {
            var state = new SimulationState();

            var bow = new GearItem() { Speed = 3.0 };

            state.Auras.Add(Aura.AimedShot);

            Assert.AreEqual(70, RangedBonusDamageCalculator.Calculate(bow, state), 0.00001);
        }
    }
}
