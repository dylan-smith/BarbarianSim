using BarbarianSim.MetaGems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarbarianSim.Tests.MetaGemTests
{
    [TestClass]
    public class PotentUnstableDiamondTests
    {
        [TestMethod]
        public void PotentUnstableDiamond()
        {
            var meta = new PotentUnstableDiamond();
            var gem = new GearItem() { Color = GemColor.Purple };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = gem });
            gearItem.Sockets.Add(new Socket(SocketColor.Meta) { Gem = meta });

            var state = new SimulationState();
            state.Config.Gear.Head = gearItem;

            state.ApplyMetaGemBonuses();

            Assert.AreEqual(24, state.Config.Gear.GetStatTotal(x => x.AttackPower));
        }

        [TestMethod]
        public void PotentUnstableDiamondOneGreen()
        {
            var meta = new PotentUnstableDiamond();
            var greenGem = new GearItem() { Color = GemColor.Green };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = greenGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Meta) { Gem = meta });

            var state = new SimulationState();
            state.Config.Gear.Head = gearItem;

            state.ApplyMetaGemBonuses();

            Assert.AreEqual(0, state.Config.Gear.GetStatTotal(x => x.AttackPower));
            Assert.AreEqual(1, state.Warnings.Count);
            Assert.AreEqual(SimulationWarnings.MetaGemInactive, state.Warnings.First());
        }
    }
}
