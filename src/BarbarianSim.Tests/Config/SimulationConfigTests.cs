using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Gems;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Config;

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
        config.Gear.Helm.Gems.Add(new RoyalSapphire(GearSlot.Helm));

        config.Gear.Chest.Aspect = new RageOfHarrogath();
        config.Gear.Chest.Gems.Add(new RoyalSapphire(GearSlot.Chest));
        config.Gear.Chest.Gems.Add(new RoyalSapphire(GearSlot.Chest));

        config.Gear.Gloves.Aspect = new GohrsDevastatingGrips();

        config.Gear.Pants.Aspect = new GhostwalkerAspect();
        config.Gear.Pants.Gems.Add(new RoyalSapphire(GearSlot.Pants));
        config.Gear.Pants.Gems.Add(new RoyalSapphire(GearSlot.Pants));

        config.Gear.Boots.Aspect = new GhostwalkerAspect();

        config.Gear.TwoHandBludgeoning.Aspect = new AspectOfGraspingWhirlwind();
        config.Gear.TwoHandBludgeoning.Expertise = Expertise.TwoHandedMace;
        config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald(GearSlot.TwoHandBludgeoning));
        config.Gear.TwoHandBludgeoning.Gems.Add(new RoyalEmerald(GearSlot.TwoHandBludgeoning));

        config.Gear.OneHandLeft.Aspect = new ConceitedAspect();
        config.Gear.OneHandLeft.Expertise = Expertise.OneHandedSword;
        config.Gear.OneHandLeft.Gems.Add(new RoyalEmerald(GearSlot.OneHandLeft));

        config.Gear.OneHandRight.Aspect = new AspectOfBerserkRipping(new Mock<SimLogger>().Object);
        config.Gear.OneHandRight.Expertise = Expertise.OneHandedSword;
        config.Gear.OneHandRight.Gems.Add(new RoyalEmerald(GearSlot.OneHandRight));

        config.Gear.TwoHandSlashing.Aspect = new AspectOfGraspingWhirlwind();
        config.Gear.TwoHandSlashing.Expertise = Expertise.TwoHandedSword;
        config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald(GearSlot.TwoHandSlashing));
        config.Gear.TwoHandSlashing.Gems.Add(new RoyalEmerald(GearSlot.TwoHandSlashing));

        config.Gear.Amulet.Aspect = new GhostwalkerAspect();
        config.Gear.Amulet.Gems.Add(new RoyalSkull(GearSlot.Amulet));

        config.Gear.Ring1.Aspect = new BoldChieftainsAspect();
        config.Gear.Ring1.Gems.Add(new RoyalSkull(GearSlot.Ring1));

        config.Gear.Ring2.Aspect = new AspectOfEchoingFury(new Mock<SimLogger>().Object);
        config.Gear.Ring2.Gems.Add(new RoyalSkull(GearSlot.Ring2));

        return config;
    }

    [Fact]
    public void GetStatTotal_Includes_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Strength = 12;
        config.Gear.Ring1.Strength = 7;

        config.GetStatTotal(x => x.Strength).Should().Be(19);
    }

    [Fact]
    public void GetStatTotal_Includes_Gems()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Strength = 12;
        config.Gear.Ring2.Gems.Add(new RoyalEmerald(GearSlot.Ring2));
        config.Gear.Ring2.Gems.First().Strength = 3;

        config.GetStatTotal(x => x.Strength).Should().Be(15);
    }

    [Fact]
    public void GetStatTotal_Includes_Paragon()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Strength = 12;
        config.Paragon.Strength = 20;

        config.GetStatTotal(x => x.Strength).Should().Be(32);
    }

    [Fact]
    public void GetStatTotal_Includes_AltersOfLilith()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Strength = 12;
        config.AltersOfLilith.Strength = 20;

        config.GetStatTotal(x => x.Strength).Should().Be(32);
    }

    [Fact]
    public void DefaultConfig_Validate_Successfully()
    {
        var config = DefaultConfig();

        var (warnings, errors) = config.Validate();

        warnings.Should().BeEmpty();
        errors.Should().BeEmpty();
    }

    [Fact]
    public void Has_Too_Many_Skill_Points_For_Level()
    {
        var config = DefaultConfig();

        config.PlayerSettings.Level = 48;

        var (warnings, _) = config.Validate();

        warnings.Should().Contain(SimulationWarnings.TooManySkillPoints);
    }

    [Fact]
    public void Not_Enough_Skill_Points()
    {
        var config = DefaultConfig();

        config.PlayerSettings.Level = 30;
        config.Skills.RemoveAll();

        config.Skills.Add(Skill.Whirlwind, 28);

        var (warnings, _) = config.Validate();

        warnings.Should().Contain(SimulationWarnings.MissingSkillPoints);
    }

    [Fact]
    public void Gear_Missing_Aspect()
    {
        var config = DefaultConfig();

        config.Gear.Helm.Aspect = null;

        var (warnings, _) = config.Validate();

        warnings.Should().Contain(SimulationWarnings.MissingAspect);
    }

    [Fact]
    public void Player_Not_Max_Level()
    {
        var config = DefaultConfig();

        config.PlayerSettings.Level = 99;

        var (warnings, _) = config.Validate();

        warnings.Should().Contain(SimulationWarnings.PlayerNotMaxLevel);
    }

    [Fact]
    public void Socket_Missing()
    {
        var config = DefaultConfig();

        config.Gear.Helm.Gems.RemoveAt(0);

        var (warnings, _) = config.Validate();

        warnings.Should().Contain(SimulationWarnings.MoreSocketsAvailable);
    }

    [Fact]
    public void Gem_Not_Max_Level()
    {
        var config = DefaultConfig();

        config.Gear.Helm.Gems.RemoveAt(0);
        config.Gear.Helm.Gems.Add(new FlawlessSapphire(GearSlot.Helm));

        var (warnings, _) = config.Validate();

        warnings.Should().Contain(SimulationWarnings.HigherLevelGemsAvailable);
    }

    [Fact]
    public void Weapon_Missing_Expertise()
    {
        var config = DefaultConfig();

        config.Gear.TwoHandSlashing.Expertise = default;

        var (warnings, _) = config.Validate();

        warnings.Should().Contain(SimulationWarnings.ExpertiseMissing);
    }
}
