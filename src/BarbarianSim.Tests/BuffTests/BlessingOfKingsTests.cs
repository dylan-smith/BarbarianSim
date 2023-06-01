using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HunterSim.Tests.Buffs
{
    [TestClass]
    public class BlessingOfKingsTests
    {
        [TestMethod]
        public void BlessingOfKings()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Buffs.Add(Buff.BlessingOfKings);

            Assert.AreEqual(Math.Floor(Constants.DRAENEI_AGI * 1.1), AgilityCalculator.Calculate(state));
            Assert.AreEqual(Math.Floor(Constants.DRAENEI_STR * 1.1), StrengthCalculator.Calculate(state));
            Assert.AreEqual(Math.Floor(Constants.DRAENEI_STA * 1.1), StaminaCalculator.Calculate(state));
            Assert.AreEqual(Math.Floor(Constants.DRAENEI_INT * 1.1), IntellectCalculator.Calculate(state));
            Assert.AreEqual(Math.Floor(Constants.DRAENEI_SPI * 1.1), SpiritCalculator.Calculate(state));
        }
    }
}
