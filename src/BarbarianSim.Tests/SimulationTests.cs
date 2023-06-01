using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace HunterSim.Tests
{
    [TestClass]
    public class SimulationTests
    {
        [TestMethod]
        public void AutoShotRotation()
        {
            var config = new DefaultConfig();
            var sim = new Simulation(config);

            RandomGenerator.Seed(64852147);

            var result = sim.Run();
            var totalDamage = result.DamageEvents.Sum(x => x.Damage);

            foreach (var e in result.ProcessedEvents)
            {
                Debug.WriteLine(e);
            }

            Assert.AreEqual(16333.5, totalDamage, 0.9);
            Assert.AreEqual(19, result.DamageEvents.Count());

            var expected = new DamageEvent(0.5, 1306.68, DamageType.Crit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(0));

            expected = new DamageEvent(3.7, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(1));

            expected = new DamageEvent(6.9, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(2));

            expected = new DamageEvent(10.1, 1306.68, DamageType.Crit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(3));

            expected = new DamageEvent(13.3, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(4));

            expected = new DamageEvent(16.5, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(5));

            expected = new DamageEvent(19.7, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(6));

            expected = new DamageEvent(22.9, 1306.68, DamageType.Crit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(7));

            expected = new DamageEvent(26.1, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(8));

            expected = new DamageEvent(29.3, 1306.68, DamageType.Crit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(9));

            expected = new DamageEvent(32.5, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(10));

            expected = new DamageEvent(35.7, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(11));

            expected = new DamageEvent(38.9, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(12));

            expected = new DamageEvent(42.1, 1306.68, DamageType.Crit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(13));

            expected = new DamageEvent(45.3, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(14));

            expected = new DamageEvent(48.5, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(15));

            expected = new DamageEvent(51.7, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(16));

            expected = new DamageEvent(54.9, 1306.68, DamageType.Crit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(17));

            expected = new DamageEvent(58.1, 653.34, DamageType.Hit, 0, 0.344, 0.656);
            AssertDamageEvent(expected, result.DamageEvents.ElementAt(18));
        }

        private void AssertDamageEvent(DamageEvent expected, DamageEvent actual)
        {
            Assert.AreEqual(expected.Damage, actual.Damage, 0.01);
            Assert.AreEqual(expected.Timestamp, actual.Timestamp, 0.01);
            Assert.AreEqual(expected.DamageType, actual.DamageType);
            Assert.AreEqual(expected.CritChance, actual.CritChance, 0.001);
            Assert.AreEqual(expected.HitChance, actual.HitChance, 0.001);
            Assert.AreEqual(expected.MissChance, actual.MissChance, 0.001);
        }
    }
}
