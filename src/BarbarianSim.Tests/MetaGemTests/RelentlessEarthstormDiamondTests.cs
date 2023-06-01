using HunterSim.MetaGems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HunterSim.Tests.MetaGemTests
{
    [TestClass]
    public class RelentlessEarthstormDiamondTests
    {
        [TestMethod]
        public void RelentlessEarthstormDiamond()
        {
            // TODO: do the multi-color gems count as both? so 1x purple + 1x green + 1x orange counts 2/2/2 red/yellow/blue?
            var meta = new RelentlessEarthstormDiamond();
            var purpleGem = new GearItem() { Color = GemColor.Purple };
            var greenGem = new GearItem() { Color = GemColor.Green };
            var orangeGem = new GearItem() { Color = GemColor.Orange };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = purpleGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = greenGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = orangeGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Meta) { Gem = meta });

            var state = new SimulationState();
            state.Config.Gear.Head = gearItem;

            state.ApplyMetaGemBonuses();

            Assert.AreEqual(12, state.Config.Gear.GetStatTotal(x => x.Agility));
            Assert.AreEqual(1, state.Auras.Count);
            Assert.AreEqual(Aura.RelentlessEarthstormDiamond, state.Auras.First());
            Assert.AreEqual(1.03, RangedCritDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.03, MeleeCritDamageMultiplierCalculator.Calculate(state));
        }

        [TestMethod]
        public void RelentlessEarthstormDiamondOneRedOneBlueOneYellow()
        {
            var meta = new RelentlessEarthstormDiamond();
            var redGem = new GearItem() { Color = GemColor.Red };
            var yellowGem = new GearItem() { Color = GemColor.Yellow };
            var blueGem = new GearItem() { Color = GemColor.Blue };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = redGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = yellowGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = blueGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Meta) { Gem = meta });

            var state = new SimulationState();
            state.Config.Gear.Head = gearItem;

            state.ApplyMetaGemBonuses();

            Assert.AreEqual(0, state.Config.Gear.GetStatTotal(x => x.Agility));
            Assert.AreEqual(1, state.Warnings.Count);
            Assert.AreEqual(SimulationWarnings.MetaGemInactive, state.Warnings.First());
            Assert.AreEqual(0, state.Auras.Count);
        }
    }
}
