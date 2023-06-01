using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class MortalShotsTests
    {
        [TestMethod]
        public void MortalShots()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Talents.Add(Talent.MortalShots, 5);

            // Mortal shots only increases the BONUS crit damage by 30%, so overall damage is multiplied by 15%
            Assert.AreEqual(1.15, RangedCritDamageMultiplierCalculator.Calculate(state), 0.001);
        }
    }
}
