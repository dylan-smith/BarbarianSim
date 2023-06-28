using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.GearSetTests
{
    [TestClass]
    public class FelstalkerArmorTests
    {
        [TestMethod]
        public void NoSetBonus()
        {
            var state = new SimulationState();

            state.ApplyGearSetBonuses();

            Assert.AreEqual(0, state.Config.Gear.GetStatTotal(x => x.AttackPower));
        }

        [TestMethod]
        public void ThreePieces()
        {
            var state = new SimulationState();

            state.Config.Gear.Waist = GearItemFactory.LoadWaist("Felstalker Belt");
            state.Config.Gear.Wrist = GearItemFactory.LoadWrist("Felstalker Bracers");
            state.Config.Gear.Chest = GearItemFactory.LoadChest("Felstalker Breastplate");

            state.ApplyGearSetBonuses();

            Assert.AreEqual(20, state.Config.Gear.GetStatTotal(x => x.HitRating));
        }
    }
}
