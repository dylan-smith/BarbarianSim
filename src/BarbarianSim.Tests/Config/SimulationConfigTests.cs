using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Gems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.Config
{
    [TestClass]
    public class SimulationConfigTests
    {
        public SimulationConfig DefaultConfig()
        {
            var config = new SimulationConfig();

            config.PlayerSettings.Level = 100;

            config.Skills.Add(Skill.LungingStrike, 1);
            config.Skills.Add(Skill.EnhancedLungingStrike, 1);
            config.Skills.Add(Skill.Whirlwind, 5);
            config.Skills.Add(Skill.EnhancedWhirlwind, 1);
            config.Skills.Add(Skill.FuriousWhirlwind, 1);
            config.Skills.Add(Skill.PressurePoint, 3);
            config.Skills.Add(Skill.RallyingCry, 5);
            config.Skills.Add(Skill.EnhancedRallyingCry, 1);
            config.Skills.Add(Skill.TacticalRallyingCry, 1);
            config.Skills.Add(Skill.IronSkin, 1);
            config.Skills.Add(Skill.EnhancedIronSkin, 1);
            config.Skills.Add(Skill.StrategicIronSkin, 1);
            config.Skills.Add(Skill.ChallengingShout, 5);
            config.Skills.Add(Skill.EnhancedChallengingShout, 1);
            config.Skills.Add(Skill.TacticalChallengingShout, 1);
            config.Skills.Add(Skill.WarCry, 5);
            config.Skills.Add(Skill.EnhancedWarCry, 1);
            config.Skills.Add(Skill.PowerWarCry, 1);
            config.Skills.Add(Skill.BoomingVoice, 3);
            config.Skills.Add(Skill.GutteralYell, 3);
            config.Skills.Add(Skill.RaidLeader, 1);
            config.Skills.Add(Skill.AggressiveResistance, 1);
            config.Skills.Add(Skill.ProlificFury, 3);
            config.Skills.Add(Skill.PitFighter, 3);
            config.Skills.Add(Skill.Hamstring, 1);
            config.Skills.Add(Skill.WrathOfTheBerserker, 1);
            config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);
            config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);
            config.Skills.Add(Skill.HeavyHanded, 3);
            config.Skills.Add(Skill.TemperedFury, 1);
            config.Skills.Add(Skill.InvigoratingFury, 3);
            config.Skills.Add(Skill.UnbridledRage, 1);

            config.Gear.Helm.Aspect = new AspectOfGraspingWhirlwind();
            config.Gear.Helm.Gems.Add(new RoyalSapphire());

            config.Gear.Chest.Aspect = new RageOfHarrogath(26);
            config.Gear.Chest.Gems.Add(new RoyalSapphire());
            config.Gear.Chest.Gems.Add(new RoyalSapphire());

            config.Gear.Gloves.Aspect = new GohrsDevastatingGrips(24);

            config.Gear.Pants.Aspect = new AspectOfNumbingWraith(12);
            config.Gear.Pants.Gems.Add(new RoyalSapphire());
            config.Gear.Pants.Gems.Add(new RoyalSapphire());

            config.Gear.Boots.Aspect = new GhostwalkerAspect(10);

            config.Gear.TwoHandBludgeoning.Aspect = new AspectOfGraspingWhirlwind();
            config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
            config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald());
            config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald());

            config.Gear.OneHandLeft.Aspect = new ConceitedAspect(25);
            config.Gear.OneHandLeft.Expertise = Expertise.OneHandedSword;
            config.Gear.OneHandLeft.Gems.Add(new RoyalEmerald());

            config.Gear.OneHandRight.Aspect = new AspectOfBerserkRipping(30);
            config.Gear.OneHandRight.Expertise = Expertise.OneHandedSword;
            config.Gear.OneHandRight.Gems.Add(new RoyalEmerald());

            config.Gear.TwoHandSlashing.Aspect = new AspectOfGraspingWhirlwind();
            config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
            config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());
            config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald());

            config.Gear.Amulet.Aspect = new EdgemastersAspect(29);
            config.Gear.Amulet.Gems.Add(new RoyalSkull());

            config.Gear.Ring1.Aspect = new BoldChieftainsAspect(1.9);
            config.Gear.Ring1.Gems.Add(new RoyalSkull());

            config.Gear.Ring2.Aspect = new AspectOfEchoingFury(4);
            config.Gear.Ring2.Gems.Add(new RoyalSkull());

            return config;
        }
        [TestMethod]
        public void DefaultConfig_Validate_Successfully()
        {
            var config = DefaultConfig();

            var (warnings, errors) = config.Validate();

            Assert.AreEqual(0, warnings.Count());
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void Has_Too_Many_Skill_Points_For_Level()
        {
            var config = DefaultConfig();

            config.PlayerSettings.Level = 48;

            var (warnings, _) = config.Validate();

            Assert.IsTrue(warnings.Contains(SimulationWarnings.TooManySkillPoints));
        }

        [TestMethod]
        public void Player_Not_Max_Level()
        {
            var config = DefaultConfig();

            config.PlayerSettings.Level = 99;

            var (warnings, _) = config.Validate();

            Assert.IsTrue(warnings.Contains(SimulationWarnings.PlayerNotMaxLevel));
        }

        [TestMethod]
        public void Not_Enough_Skill_Points()
        {
            var config = DefaultConfig();

            config.PlayerSettings.Level = 30;
            config.Skills.RemoveAll();

            config.Skills.Add(Skill.Whirlwind, 28);

            var (warnings, _) = config.Validate();

            Assert.IsTrue(warnings.Contains(SimulationWarnings.MissingSkillPoints));
        }

        [TestMethod]
        public void Gear_Missing_Aspect()
        {
            var config = DefaultConfig();

            config.Gear.Helm.Aspect = null;

            var (warnings, _) = config.Validate();

            Assert.IsTrue(warnings.Contains(SimulationWarnings.MissingAspect));
        }
    }
}
