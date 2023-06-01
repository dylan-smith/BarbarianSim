using HunterSim.MetaGems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HunterSim.Tests.MetaGemTests
{
    [TestClass]
    public class SwiftWindfireDiamondTests
    {
        [TestMethod]
        public void SwiftWindfireDiamond()
        {
            // TODO: do the multi-color gems count as both? so 1x purple + 1x green + 1x orange counts 2/2/2 red/yellow/blue?
            var meta = new SwiftWindfireDiamond();
            var yellowGem = new GearItem() { Color = GemColor.Yellow };
            var redGem = new GearItem() { Color = GemColor.Red };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = yellowGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = yellowGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = redGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Meta) { Gem = meta });

            var state = new SimulationState();
            state.Config.Gear.Head = gearItem;

            state.ApplyMetaGemBonuses();

            Assert.AreEqual(20, state.Config.Gear.GetStatTotal(x => x.AttackPower));
        }

        [TestMethod]
        public void SwiftWindfireDiamondOneYellowOneRed()
        {
            var meta = new SwiftWindfireDiamond();
            var redGem = new GearItem() { Color = GemColor.Red };
            var yellowGem = new GearItem() { Color = GemColor.Yellow };
            var gearItem = new GearItem();
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = redGem });
            gearItem.Sockets.Add(new Socket(SocketColor.Red) { Gem = yellowGem });
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
