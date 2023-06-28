using BarbarianSim.MetaGems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarbarianSim.Tests.MetaGemTests
{
    [TestClass]
    public class EnigmaticSkyfireDiamondTests
    {
        [TestMethod]
        public void EnigmaticSkyfireDiamond()
        {
            var meta = new EnigmaticSkyfireDiamond();
            var gem = new GearItem() { Color = GemColor.Red };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = gem });
            gearItem.Sockets.Add(new Socket(SocketColor.Meta) { Gem = meta });

            var state = new SimulationState();
            state.Config.Gear.Head = gearItem;

            state.ApplyMetaGemBonuses();

            Assert.AreEqual(12, state.Config.Gear.GetStatTotal(x => x.CritRating));
        }

        [TestMethod]
        public void EnigmaticSkyfireDiamondOneYellowOneRed()
        {
            var meta = new EnigmaticSkyfireDiamond();
            var redGem = new GearItem() { Color = GemColor.Red };
            var yellowGem = new GearItem() { Color = GemColor.Yellow };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = redGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = yellowGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Meta) { Gem = meta });

            var state = new SimulationState();
            state.Config.Gear.Head = gearItem;

            state.ApplyMetaGemBonuses();

            Assert.AreEqual(0, state.Config.Gear.GetStatTotal(x => x.CritRating));
            Assert.AreEqual(1, state.Warnings.Count);
            Assert.AreEqual(SimulationWarnings.MetaGemInactive, state.Warnings.First());
        }
    }
}
