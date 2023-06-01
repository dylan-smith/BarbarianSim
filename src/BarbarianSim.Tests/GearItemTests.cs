using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HunterSim.Tests
{
    [TestClass]
    public class GearItemTests
    {
        [TestMethod]
        public void TotalStrength()
        {
            var gear = new GearItem
            {
                Strength = 7
            };

            Assert.AreEqual(7, gear.GetStatWithSockets(x => x.Strength));
        }

        [TestMethod]
        public void TotalStrengthWithEmptySockets()
        {
            var gear = new GearItem
            {
                Strength = 7
            };
            gear.Sockets.Add(new Socket(SocketColor.Red));

            Assert.AreEqual(7, gear.GetStatWithSockets(x => x.Strength));
        }

        [TestMethod]
        public void TotalStrengthWithGems()
        {
            var gear = new GearItem
            {
                Strength = 7
            };

            var gem1 = new GearItem
            {
                Strength = 3,
                Agility = 27,
                Color = GemColor.Red
            };

            var gem2 = new GearItem
            {
                Strength = 6,
                Defense = 12,
                Color = GemColor.Red
            };

            gear.Sockets.Add(new Socket(SocketColor.Red) { Gem = gem1 });
            gear.Sockets.Add(new Socket(SocketColor.Red) { Gem = gem2 });

            Assert.AreEqual(16, gear.GetStatWithSockets(x => x.Strength));
        }

        [TestMethod]
        public void TotalStrengthWithActiveSocketBonus()
        {
            var gear = new GearItem
            {
                Strength = 7
            };

            var gem1 = new GearItem
            {
                Strength = 3,
                Agility = 27,
                Color = GemColor.Red
            };

            var gem2 = new GearItem
            {
                Strength = 6,
                Defense = 12,
                Color = GemColor.Blue
            };

            gear.Sockets.Add(new Socket(SocketColor.Red) { Gem = gem1 });
            gear.Sockets.Add(new Socket(SocketColor.Blue) { Gem = gem2 });

            var bonus = new GearItem
            {
                Agility = 11,
                Strength = 33
            };

            gear.SocketBonus = bonus;

            Assert.AreEqual(49, gear.GetStatWithSockets(x => x.Strength));
        }

        [TestMethod]
        public void TotalStrengthWithInactiveSocketBonus()
        {
            var gear = new GearItem
            {
                Strength = 7
            };

            var gem1 = new GearItem
            {
                Strength = 3,
                Agility = 27,
                Color = GemColor.Red
            };

            var gem2 = new GearItem
            {
                Strength = 6,
                Defense = 12,
                Color = GemColor.Yellow
            };

            gear.Sockets.Add(new Socket(SocketColor.Red) { Gem = gem1 });
            gear.Sockets.Add(new Socket(SocketColor.Blue) { Gem = gem2 });

            var bonus = new GearItem
            {
                Agility = 11,
                Strength = 33
            };

            gear.SocketBonus = bonus;

            Assert.AreEqual(16, gear.GetStatWithSockets(x => x.Strength));
        }
    }
}
