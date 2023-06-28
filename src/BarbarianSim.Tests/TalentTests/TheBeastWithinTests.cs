using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class TheBeastWithinTests
    {
        [TestMethod]
        public void TheBeastWithin()
        {
            var state = new SimulationState();
            // state.Config.Talents.Add(Talent.TheBeastWithin, 1);

            state.Auras.Add(Aura.TheBeastWithin);

            Assert.AreEqual(1.1, DamageMultiplierCalculator.Calculate(state), 0.00001);
        }

        // TODO: need to implement the Bestial Wrath ability and have it apply this aura if specced into it
        // TODO: need to test the events and cooldown/length stuff too
        // TODO: test the mana reducing effect
    }
}
