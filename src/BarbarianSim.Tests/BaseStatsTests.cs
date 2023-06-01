using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HunterSim.Tests
{
    [TestClass]
    public class BaseStatsTests
    {
        [TestMethod]
        public void Dwarf()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Dwarf;

            Assert.AreEqual(66, StrengthCalculator.Calculate(state));
            Assert.AreEqual(147, AgilityCalculator.Calculate(state));
            Assert.AreEqual(111, StaminaCalculator.Calculate(state));
            Assert.AreEqual(76, IntellectCalculator.Calculate(state));
            Assert.AreEqual(82, SpiritCalculator.Calculate(state));
            // Base Health + Stamina * 10
            Assert.AreEqual(3488 + 1110, HealthCalculator.Calculate(state));
            // Base Mana + Intellect * 15
            Assert.AreEqual(3253 + 1140, ManaCalculator.Calculate(state));
        }

        [TestMethod]
        public void NightElf()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.NightElf;

            Assert.AreEqual(61, StrengthCalculator.Calculate(state));
            Assert.AreEqual(156, AgilityCalculator.Calculate(state));
            Assert.AreEqual(107, StaminaCalculator.Calculate(state));
            Assert.AreEqual(77, IntellectCalculator.Calculate(state));
            Assert.AreEqual(83, SpiritCalculator.Calculate(state));
            // Base Health + Stamina * 10
            Assert.AreEqual(3488 + 1070, HealthCalculator.Calculate(state));
            // Base Mana + Intellect * 15
            Assert.AreEqual(3253 + 1155, ManaCalculator.Calculate(state));
        }

        [TestMethod]
        public void Orc()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Orc;

            Assert.AreEqual(67, StrengthCalculator.Calculate(state));
            Assert.AreEqual(148, AgilityCalculator.Calculate(state));
            Assert.AreEqual(110, StaminaCalculator.Calculate(state));
            Assert.AreEqual(74, IntellectCalculator.Calculate(state));
            Assert.AreEqual(86, SpiritCalculator.Calculate(state));
            // Base Health + Stamina * 10
            Assert.AreEqual(3488 + 1100, HealthCalculator.Calculate(state));
            // Base Mana + Intellect * 15
            Assert.AreEqual(3253 + 1110, ManaCalculator.Calculate(state));
        }

        [TestMethod]
        public void Tauren()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Tauren;

            Assert.AreEqual(69, StrengthCalculator.Calculate(state));
            Assert.AreEqual(146, AgilityCalculator.Calculate(state));
            Assert.AreEqual(110, StaminaCalculator.Calculate(state));
            Assert.AreEqual(72, IntellectCalculator.Calculate(state));
            Assert.AreEqual(85, SpiritCalculator.Calculate(state));
            // Base Health + Stamina * 10
            Assert.AreEqual(3488 + 1100, HealthCalculator.Calculate(state));
            // Base Mana + Intellect * 15
            Assert.AreEqual(3253 + 1080, ManaCalculator.Calculate(state));
        }

        [TestMethod]
        public void Troll()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Troll;

            Assert.AreEqual(65, StrengthCalculator.Calculate(state));
            Assert.AreEqual(153, AgilityCalculator.Calculate(state));
            Assert.AreEqual(109, StaminaCalculator.Calculate(state));
            Assert.AreEqual(73, IntellectCalculator.Calculate(state));
            Assert.AreEqual(84, SpiritCalculator.Calculate(state));
            // Base Health + Stamina * 10
            Assert.AreEqual(3488 + 1090, HealthCalculator.Calculate(state));
            // Base Mana + Intellect * 15
            Assert.AreEqual(3253 + 1095, ManaCalculator.Calculate(state));
        }

        [TestMethod]
        public void Draenei()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;

            Assert.AreEqual(65, StrengthCalculator.Calculate(state));
            Assert.AreEqual(148, AgilityCalculator.Calculate(state));
            Assert.AreEqual(107, StaminaCalculator.Calculate(state));
            Assert.AreEqual(78, IntellectCalculator.Calculate(state));
            Assert.AreEqual(85, SpiritCalculator.Calculate(state));
            // Base Health + Stamina * 10
            Assert.AreEqual(3488 + 1070, HealthCalculator.Calculate(state));
            // Base Mana + Intellect * 15
            Assert.AreEqual(3253 + 1170, ManaCalculator.Calculate(state));
        }

        [TestMethod]
        public void BloodElf()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.BloodElf;

            Assert.AreEqual(61, StrengthCalculator.Calculate(state));
            Assert.AreEqual(153, AgilityCalculator.Calculate(state));
            Assert.AreEqual(106, StaminaCalculator.Calculate(state));
            Assert.AreEqual(81, IntellectCalculator.Calculate(state));
            Assert.AreEqual(82, SpiritCalculator.Calculate(state));
            // Base Health + Stamina * 10
            Assert.AreEqual(3488 + 1060, HealthCalculator.Calculate(state));
            // Base Mana + Intellect * 15
            Assert.AreEqual(3253 + 1215, ManaCalculator.Calculate(state));
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void NoRace()
        {
            var state = new SimulationState();

            AgilityCalculator.Calculate(state);
        }

        [TestMethod]
        public void OtherStats()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;

            var testWeapon = new GearItem() { Speed = 3.0 };

            Assert.AreEqual(0.0, ArcaneResistanceCalculator.Calculate(state));
            // Base Agility * 2
            Assert.AreEqual(296.0, ArmorCalculator.Calculate(state));
            Assert.AreEqual(0.0, RangedBonusDamageCalculator.Calculate(testWeapon, state));
            Assert.AreEqual(1.0, DamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.0, RangedDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(0.0, FireResistanceCalculator.Calculate(state));
            Assert.AreEqual(0.0, FrostResistanceCalculator.Calculate(state));
            // Base Strength + Agi + 120 base MAP
            Assert.AreEqual(65 + 148 + 120, MeleeAttackPowerCalculator.Calculate(state));
            // TODO: Not sure this is right, is melee base crit the same as base ranged crit?
            Assert.AreEqual(0.00, MeleeCritCalculator.Calculate(state));
            Assert.AreEqual(1.0, MeleeCritDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.0, MeleeHasteCalculator.Calculate(state));
            Assert.AreEqual(0.09, MissChanceCalculator.Calculate(new GearItem() { WeaponType = WeaponType.Gun }, state), 0.0001);
            Assert.AreEqual(0.09, MissChanceCalculator.Calculate(new GearItem() { WeaponType = WeaponType.OneHandedSword }, state), 0.0001);
            Assert.AreEqual(1.0, MovementSpeedCalculator.Calculate(state));
            Assert.AreEqual(0.0, MP5Calculator.Calculate(state));
            Assert.AreEqual(0.0, NatureResistanceCalculator.Calculate(state));
            // Base Agility + 130 base RAP
            Assert.AreEqual(148 + 130, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(0.0, RangedCritCalculator.Calculate(state));
            Assert.AreEqual(1.0, RangedCritDamageMultiplierCalculator.Calculate(state));
            Assert.AreEqual(1.0, RangedHasteCalculator.Calculate(state));
            Assert.AreEqual(0.0, ShadowResistanceCalculator.Calculate(state));
            Assert.AreEqual(0.0, SpellCritCalculator.Calculate(state));
            Assert.AreEqual(350, WeaponSkillCalculator.Calculate(new GearItem() { WeaponType = WeaponType.Gun }, state));
        }
    }
}
