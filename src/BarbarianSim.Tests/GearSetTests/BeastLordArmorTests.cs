using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests.GearSetTests
{
    [TestClass]
    public class BeastLordArmorTests
    {
        [TestMethod]
        public void NoSetBonus()
        {
            var state = new SimulationState();

            state.ApplyGearSetBonuses();

            Assert.IsTrue(!state.Auras.Contains(Aura.TrapCooldown));
            Assert.IsTrue(!state.Auras.Contains(Aura.ImprovedKillCommand));
        }

        [TestMethod]
        public void TwoPieces()
        {
            var state = new SimulationState();

            state.Config.Gear.Head = GearItemFactory.LoadHead("Beast Lord Helm");
            state.Config.Gear.Shoulder = GearItemFactory.LoadShoulder("Beast Lord Mantle");
            state.Config.Gear.Hands = GearItemFactory.LoadHands("Beast Lord Handguards");

            state.ApplyGearSetBonuses();

            Assert.IsTrue(state.Auras.Contains(Aura.TrapCooldown));
            Assert.IsTrue(!state.Auras.Contains(Aura.ImprovedKillCommand));
        }

        [TestMethod]
        public void FourPieces()
        {
            var state = new SimulationState();

            state.Config.Gear.Head = GearItemFactory.LoadHead("Beast Lord Helm");
            state.Config.Gear.Shoulder = GearItemFactory.LoadShoulder("Beast Lord Mantle");
            state.Config.Gear.Hands = GearItemFactory.LoadHands("Beast Lord Handguards");
            state.Config.Gear.Chest = GearItemFactory.LoadChest("Beast Lord Cuirass");

            state.ApplyGearSetBonuses();

            Assert.IsTrue(state.Auras.Contains(Aura.TrapCooldown));
            Assert.IsTrue(state.Auras.Contains(Aura.ImprovedKillCommand));
        }
    }
}
