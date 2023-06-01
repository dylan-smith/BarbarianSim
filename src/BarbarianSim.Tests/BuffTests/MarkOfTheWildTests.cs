using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class MarkOfTheWildTests
    {
        [TestMethod]
        public void MarkOfTheWild()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.MarkOfTheWild);

            Assert.AreEqual(((Constants.DRAENEI_AGI + 14) * 2) + 340, ArmorCalculator.Calculate(state));

            Assert.AreEqual(Constants.DRAENEI_AGI + 14, AgilityCalculator.Calculate(state), 0.001);
            Assert.AreEqual(Constants.DRAENEI_STR + 14, StrengthCalculator.Calculate(state), 0.001);
            Assert.AreEqual(Constants.DRAENEI_STA + 14, StaminaCalculator.Calculate(state), 0.001);
            Assert.AreEqual(Constants.DRAENEI_INT + 14, IntellectCalculator.Calculate(state), 0.001);
            Assert.AreEqual(Constants.DRAENEI_SPI + 14, SpiritCalculator.Calculate(state), 0.001);

            Assert.AreEqual(25, ArcaneResistanceCalculator.Calculate(state));
            Assert.AreEqual(25, FireResistanceCalculator.Calculate(state));
            Assert.AreEqual(25, FrostResistanceCalculator.Calculate(state));
            Assert.AreEqual(25, NatureResistanceCalculator.Calculate(state));
            Assert.AreEqual(25, ShadowResistanceCalculator.Calculate(state));
        }
    }
}
