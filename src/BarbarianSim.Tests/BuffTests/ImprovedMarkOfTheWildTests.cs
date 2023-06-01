using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class ImprovedMarkOfTheWildTests
    {
        [TestMethod]
        public void ImprovedMarkOfTheWild()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.ImprovedMarkOfTheWild);

            Assert.AreEqual(((Constants.DRAENEI_AGI + 18) * 2) + 459, ArmorCalculator.Calculate(state));

            Assert.AreEqual(Constants.DRAENEI_AGI + 18, AgilityCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_STR + 18, StrengthCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_STA + 18, StaminaCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_INT + 18, IntellectCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_SPI + 18, SpiritCalculator.Calculate(state));

            Assert.AreEqual(33, ArcaneResistanceCalculator.Calculate(state));
            Assert.AreEqual(33, FireResistanceCalculator.Calculate(state));
            Assert.AreEqual(33, FrostResistanceCalculator.Calculate(state));
            Assert.AreEqual(33, NatureResistanceCalculator.Calculate(state));
            Assert.AreEqual(33, ShadowResistanceCalculator.Calculate(state));
        }
    }
}
