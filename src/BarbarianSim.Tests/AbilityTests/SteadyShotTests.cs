using BarbarianSim.Abilities;
using BarbarianSim.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BarbarianSim.Tests.AbilityTests
{
    [TestClass]
    public class SteadyShotTests
    {
        [TestInitialize]
        public void TestInitialize() => InjectZeroMocks();

        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
            RandomGenerator.ClearMock();
        }

        [TestMethod]
        public void SteadyShotCast()
        {
            var state = new SimulationState
            {
                CurrentTime = 7.2
            };

            Assert.IsTrue(SteadyShot.CanUse(state));

            SteadyShot.Use(state);

            Assert.AreEqual(1, state.Events.Count);
            Assert.AreEqual(7.2, state.Events[0].Timestamp, 0.001);
            Assert.AreEqual(typeof(SteadyShotCastEvent), state.Events[0].GetType());
        }

        [TestMethod]
        public void SteadyShotCantUseOnGCD()
        {
            var state = new SimulationState();
            state.Auras.Add(Aura.GlobalCooldown);

            Assert.IsFalse(SteadyShot.CanUse(state));
        }

        [TestMethod]
        public void SteadyShotCantUseWhileCasting()
        {
            var state = new SimulationState();
            state.Auras.Add(Aura.CastInProgress);

            Assert.IsFalse(SteadyShot.CanUse(state));
        }

        [TestMethod]
        public void SteadyShotCastEvent()
        {
            var state = new SimulationState();

            var e = new SteadyShotCastEvent(7.2);

            e.ProcessEvent(state);

            Assert.IsTrue(state.Auras.Contains(Aura.CastInProgress));
            Assert.IsTrue(state.Auras.Contains(Aura.GlobalCooldown));
            
            Assert.AreEqual(2, state.Events.Count);

            Assert.IsTrue(state.Events.Any(x => x is SteadyShotCompletedEvent));
            Assert.IsTrue(state.Events.Any(x => x is GlobalCooldownExpiredEvent));

            Assert.AreEqual(8.7, state.Events.First(x => x is SteadyShotCompletedEvent).Timestamp, 0.001);
            Assert.AreEqual(8.7, state.Events.First(x => x is GlobalCooldownExpiredEvent).Timestamp, 0.001);
        }

        //// TODO: Test that AutoShot takes haste into account
        //[TestMethod]
        //public void AutoShotCastWithHaste()
        //{
        //    var state = new SimulationState();
        //    state.Config.Gear.Ranged = new GearItem
        //    {
        //        Speed = 2.9,
        //    };

        //    BaseStatCalculator.InjectMock(typeof(RangedHasteCalculator), new FakeStatCalculator(1.2));

        //    var e = new AutoShotCastEvent(7.2);

        //    e.ProcessEvent(state);

        //    Assert.IsTrue(state.Auras.Contains(Aura.AutoShotOnCooldown));
        //    Assert.AreEqual(2, state.Events.Count);

        //    var firstEvent = state.Events.OrderBy(x => x.Timestamp).First();
        //    var secondEvent = state.Events.OrderBy(x => x.Timestamp).Last();

        //    Assert.AreEqual(typeof(AutoShotCompletedEvent), firstEvent.GetType());
        //    Assert.AreEqual(typeof(AutoShotCooldownCompletedEvent), secondEvent.GetType());

        //    Assert.AreEqual(7.2 + (0.5 / 1.2), firstEvent.Timestamp, 0.001);
        //    Assert.AreEqual(7.2 + (2.9 / 1.2), secondEvent.Timestamp, 0.001);
        //}

        //[TestMethod]
        //public void AutoShotCooldownCompletedEvent()
        //{
        //    var state = new SimulationState();
        //    state.Auras.Add(Aura.AutoShotOnCooldown);

        //    var e = new AutoShotCooldownCompletedEvent(10.1);

        //    e.ProcessEvent(state);

        //    Assert.IsFalse(state.Auras.Contains(Aura.AutoShotOnCooldown));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void AutoShotCooldownCompletedEventAuraMissing()
        //{
        //    var state = new SimulationState();

        //    var e = new AutoShotCooldownCompletedEvent(10.1);

        //    e.ProcessEvent(state);
        //}

        [TestMethod]
        public void SteadyShotCompletedEventHit()
        {
            var state = new SimulationState();
            
            var e = new SteadyShotCompletedEvent(7.7);

            e.ProcessEvent(state);

            Assert.IsFalse(state.Auras.Contains(Aura.CastInProgress));

            Assert.AreEqual(1, state.Events.Count);
            Assert.IsTrue(state.Events[0] is DamageEvent);

            var dmg = (DamageEvent)state.Events[0];

            Assert.AreEqual(7.7, dmg.Timestamp, 0.001);
            Assert.AreEqual(150, dmg.Damage);
            Assert.AreEqual(DamageType.Hit, dmg.DamageType);
            Assert.AreEqual(0.00, dmg.MissChance);
            Assert.AreEqual(0.00, dmg.CritChance);
            Assert.AreEqual(1.0, dmg.HitChance);
        }

        //[TestMethod]
        //public void AutoShotCompletedEventMiss()
        //{
        //    var state = new SimulationState();
        //    state.Config.Gear.Ranged = new GearItem
        //    {
        //        Speed = 2.9,
        //        WeaponType = WeaponType.Bow,
        //        MinDamage = 100,
        //        MaxDamage = 200,
        //    };

        //    BaseStatCalculator.InjectMock(typeof(MissChanceCalculator), new FakeStatCalculator(0.09));
        //    RandomGenerator.InjectMock(new FakeRandomGenerator(0.089));

        //    var e = new AutoShotCompletedEvent(7.7);

        //    e.ProcessEvent(state);

        //    Assert.AreEqual(1, state.Events.Count);
        //    Assert.AreEqual(typeof(DamageEvent), state.Events[0].GetType());

        //    var dmg = (DamageEvent)state.Events[0];

        //    Assert.AreEqual(7.7, dmg.Timestamp, 0.001);
        //    Assert.AreEqual(0, dmg.Damage);
        //    Assert.AreEqual(DamageType.Miss, dmg.DamageType);
        //    Assert.AreEqual(0.09, dmg.MissChance, 0.001);
        //    Assert.AreEqual(0.00, dmg.CritChance, 0.001);
        //    Assert.AreEqual(0.91, dmg.HitChance, 0.001);
        //}

        //[TestMethod]
        //public void AutoShotCompletedEventCrit()
        //{
        //    var state = new SimulationState();
        //    state.Config.Gear.Ranged = new GearItem
        //    {
        //        Speed = 2.9,
        //        WeaponType = WeaponType.Bow,
        //        MinDamage = 100,
        //        MaxDamage = 200,
        //    };

        //    BaseStatCalculator.InjectMock(typeof(MissChanceCalculator), new FakeStatCalculator(0.09));
        //    BaseStatCalculator.InjectMock(typeof(RangedCritCalculator), new FakeStatCalculator(0.20));
        //    RandomGenerator.InjectMock(new FakeRandomGenerator(0.091, 0.19));

        //    var e = new AutoShotCompletedEvent(7.7);

        //    e.ProcessEvent(state);

        //    Assert.AreEqual(1, state.Events.Count);
        //    Assert.AreEqual(typeof(DamageEvent), state.Events[0].GetType());

        //    var dmg = (DamageEvent)state.Events[0];

        //    Assert.AreEqual(7.7, dmg.Timestamp, 0.01);
        //    Assert.AreEqual(300, dmg.Damage);
        //    Assert.AreEqual(DamageType.Crit, dmg.DamageType);
        //    Assert.AreEqual(0.09, dmg.MissChance, 0.001);
        //    Assert.AreEqual(0.182, dmg.CritChance, 0.0001);
        //    Assert.AreEqual(0.728, dmg.HitChance, 0.0001);
        //}

        private void InjectZeroMocks()
        {
            var zeroMock = new FakeStatCalculator(0.0);

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(ArcaneResistanceCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(ArmorCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(RangedBonusDamageCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(DamageMultiplierCalculator), new FakeStatCalculator(1.0));
            BaseStatCalculator.InjectMock(typeof(FireResistanceCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(FrostResistanceCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(HealthCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(IntellectCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(ManaCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(MeleeAttackPowerCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(MeleeCritCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(MeleeCritDamageMultiplierCalculator), new FakeStatCalculator(1.0));
            BaseStatCalculator.InjectMock(typeof(MeleeHasteCalculator), new FakeStatCalculator(1.0));
            BaseStatCalculator.InjectMock(typeof(MissChanceCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(MovementSpeedCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(MP5Calculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(NatureResistanceCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(RangedAttackPowerCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(RangedCritCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(RangedCritDamageMultiplierCalculator), new FakeStatCalculator(1.0));
            BaseStatCalculator.InjectMock(typeof(RangedHasteCalculator), new FakeStatCalculator(1.0));
            BaseStatCalculator.InjectMock(typeof(ShadowResistanceCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(SpellCritCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(SpiritCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(StaminaCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(StrengthCalculator), zeroMock);
            BaseStatCalculator.InjectMock(typeof(WeaponSkillCalculator), new FakeStatCalculator(300));
        }
    }
}
