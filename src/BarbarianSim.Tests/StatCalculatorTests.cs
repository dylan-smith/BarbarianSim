using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HunterSim.Tests
{
    [TestClass]
    public class StatCalculatorTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
        }

        [TestMethod]
        public void StrengthCalculatorWithGemsAndSocketBonus()
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

            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.Gear.Head = gear;

            Assert.AreEqual(Constants.DRAENEI_STR + 49, StrengthCalculator.Calculate(state));
        }

        [TestMethod]
        public void DodgeCalculatorBaseStats()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;

            Assert.AreEqual(Constants.DRAENEI_AGI / 2500, DodgeCalculator.Calculate(state), 0.000001);
        }

        [TestMethod]
        public void CritCalculatorBaseStats()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;

            Assert.AreEqual(0.0, RangedCritCalculator.Calculate(state), 0.000001);
            Assert.AreEqual(0.0, MeleeCritCalculator.Calculate(state), 0.000001);
        }

        [TestMethod]
        public void CritCalculatorSuppressionAmount()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT));
            Assert.AreEqual(0.0, RangedCritCalculator.Calculate(state));
            Assert.AreEqual(0.0, MeleeCritCalculator.Calculate(state));

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT + 1));
            Assert.IsTrue(RangedCritCalculator.Calculate(state) > 0.0);
            Assert.IsTrue(MeleeCritCalculator.Calculate(state) > 0.0);
        }

        [TestMethod]
        public void CritCalculatorAgilityToCrit()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT + 80));

            // https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview
            Assert.AreEqual(0.02, RangedCritCalculator.Calculate(state), 0.00001);
            Assert.AreEqual(0.02, MeleeCritCalculator.Calculate(state), 0.00001);
        }

        [TestMethod]
        public void CritCalculatorRatingToCrit()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT));

            state.Config.Gear.Head = new GearItem() { CritRating = 44.16 };

            // https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview
            Assert.AreEqual(0.02, RangedCritCalculator.Calculate(state), 0.00001);
            Assert.AreEqual(0.02, MeleeCritCalculator.Calculate(state), 0.00001);
        }

        [TestMethod]
        public void MissChanceCalculatorRatingToHit()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;

            BaseStatCalculator.InjectMock(typeof(WeaponSkillCalculator), new FakeStatCalculator(350));

            state.Config.Gear.Head = new GearItem() { HitRating = 46 };

            // https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview
            Assert.AreEqual(0.09 - 0.02911392, MissChanceCalculator.Calculate(null, state), 0.00001);
        }

        [TestMethod]
        public void RangedHasteCalculatorRatingToPercent()
        {
            var state = new SimulationState();

            state.Config.Gear.Head = new GearItem() { HasteRating = 300 };

            // https://tbc.wowhead.com/guides/classic-the-burning-crusade-stats-overview
            Assert.AreEqual(1.1899, RangedHasteCalculator.Calculate(state), 0.0001);
            Assert.AreEqual(1.1899, MeleeHasteCalculator.Calculate(state), 0.0001);
        }

        // Strength - Sixx has a bug with improved strength of earth totem
        // Agility - Sixx doesn't round down aggressively enough, difference of 1
        // Sixx also doesn't appear to take into account the talent Survival Instincts anywhere

        [TestMethod]
        public void DefaultConfigNoBuffs()
        {
            var state = new SimulationState
            {
                Config = new DefaultConfig()
            };

            state.Config.Buffs.Clear();
            // the mark debuff isn't included in the in-game state sheet (obv)
            state.Config.Talents[Talent.ImprovedHuntersMark] = 0;

            state.Validate();

            Assert.AreEqual(71, StrengthCalculator.Calculate(state));
            Assert.AreEqual(769, AgilityCalculator.Calculate(state));
            Assert.AreEqual(421, StaminaCalculator.Calculate(state));
            Assert.AreEqual(159, IntellectCalculator.Calculate(state));
            Assert.AreEqual(91, SpiritCalculator.Calculate(state));
            Assert.AreEqual(4636, ArmorCalculator.Calculate(state));
            Assert.AreEqual(1875, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(1938, MeleeAttackPowerCalculator.Calculate(state));
            // subtracting out the crit suppression that the sim includes but the stat page in game doesn't
            Assert.AreEqual(0.3009 - 0.048, RangedCritCalculator.Calculate(state), 0.0001);
            Assert.AreEqual(0.2382 - 0.048, MeleeCritCalculator.Calculate(state), 0.0001);
        }

        [TestMethod]
        public void DefaultConfigNoBuffsNoTalents()
        {
            var state = new SimulationState
            {
                Config = new DefaultConfig()
            };

            state.Config.Buffs.Clear();
            state.Config.Talents.Clear();

            state.Validate();

            Assert.AreEqual(71, StrengthCalculator.Calculate(state));
            Assert.AreEqual(669, AgilityCalculator.Calculate(state));
            Assert.AreEqual(421, StaminaCalculator.Calculate(state));
            Assert.AreEqual(159, IntellectCalculator.Calculate(state));
            Assert.AreEqual(91, SpiritCalculator.Calculate(state));
            Assert.AreEqual(4436, ArmorCalculator.Calculate(state));
            Assert.AreEqual(1703, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(1764, MeleeAttackPowerCalculator.Calculate(state));
            // subtracting out the crit suppression that the sim includes but the stat page in game doesn't
            Assert.AreEqual(0.1959 - 0.048, RangedCritCalculator.Calculate(state), 0.0001);
            Assert.AreEqual(0.1832 - 0.048, MeleeCritCalculator.Calculate(state), 0.0001);

            
        }

        [TestMethod]
        public void NoGearNoBuffsNoTalents()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            // intentionally setting boss to 70 to avoid the 3% crit suppression on raid bosses
            state.Config.BossSettings.Level = 70;

            // numbers taken from in game stat page with no gear and no talents
            Assert.AreEqual(278, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(333, MeleeAttackPowerCalculator.Calculate(state));
            // in-game it says 2.17% but the sim subtracts 1.8% for crit suppression aura
            Assert.AreEqual(0.0217 - 0.018, RangedCritCalculator.Calculate(state), 0.000001);
            Assert.AreEqual(0.0217 - 0.018, MeleeCritCalculator.Calculate(state), 0.000001);
            Assert.AreEqual(65, StrengthCalculator.Calculate(state));
            Assert.AreEqual(148, AgilityCalculator.Calculate(state));
            Assert.AreEqual(107, StaminaCalculator.Calculate(state));
            Assert.AreEqual(78, IntellectCalculator.Calculate(state));
            Assert.AreEqual(85, SpiritCalculator.Calculate(state));
            Assert.AreEqual(296, ArmorCalculator.Calculate(state));
        }

        // 155 RAP for aspect of the hawk
        // 130 base RAP ???

        // TODO: Tests for all calculators that need to convert between rating and %
        // TODO: Should probably have a test for each calculator for base stats
        // TODO: Test for enchants
    }
}