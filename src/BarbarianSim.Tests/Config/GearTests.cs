using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Gems;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Config;

public class GearTests
{
    [Fact]
    public void AllGear_Returns_12_Items()
    {
        var gear = new Gear();
        gear.AllGear.Should().HaveCount(12);
    }

    [Fact]
    public void GetAllGems_Returns_0_Gems()
    {
        var gear = new Gear();
        gear.GetAllGems().Should().BeEmpty();
    }

    [Fact]
    public void GetAllGems_Returns_All_Gems()
    {
        var gear = new Gear();
        gear.Helm.Gems.Add(new RoyalSapphire(GearSlot.Helm));
        gear.Helm.Gems.Add(new RoyalSapphire(GearSlot.Helm));
        gear.Chest.Gems.Add(new RoyalSapphire(GearSlot.Chest));
        gear.Amulet.Gems.Add(new RoyalSapphire(GearSlot.Amulet));

        gear.GetAllGems().Should().HaveCount(4);
    }

    [Fact]
    public void GetAllAspects_Returns_All_Aspects()
    {
        var gear = new Gear();
        gear.Helm.Aspect = new AspectOfEchoingFury(new Mock<SimLogger>().Object);
        gear.Chest.Aspect = new AspectOfDisobedience(new Mock<SimLogger>().Object);
        gear.Amulet.Aspect = null;

        gear.GetAllAspects<Aspect>().Should().HaveCount(2);
    }

    [Fact]
    public void GetAllAspects_Filters_To_A_Specific_Aspect()
    {
        var gear = new Gear();
        gear.Helm.Aspect = new AspectOfEchoingFury(new Mock<SimLogger>().Object);
        gear.Chest.Aspect = new AspectOfDisobedience(new Mock<SimLogger>().Object) { ArmorIncrement = 0.25 };
        gear.Amulet.Aspect = null;

        gear.GetAllAspects<AspectOfDisobedience>().Should().HaveCount(1);
        gear.GetAllAspects<AspectOfDisobedience>().First().ArmorIncrement.Should().Be(0.25);
    }

    [Fact]
    public void GetStatTotal_Returns_0()
    {
        var gear = new Gear();
        gear.GetStatTotal(g => g.Strength).Should().Be(0);
    }

    [Fact]
    public void GetStatTotal_Adds_Up_All_Gear()
    {
        var gear = new Gear();
        gear.Helm.Strength = 1;
        gear.Chest.Strength = 1;
        gear.Gloves.Strength = 1;
        gear.Pants.Strength = 1;
        gear.Boots.Strength = 1;
        gear.TwoHandBludgeoning.Strength = 1;
        gear.OneHandLeft.Strength = 1;
        gear.OneHandRight.Strength = 1;
        gear.TwoHandSlashing.Strength = 1;
        gear.Amulet.Strength = 1;
        gear.Ring1.Strength = 1;
        gear.Ring2.Strength = 1;

        gear.GetStatTotal(g => g.Strength).Should().Be(12);
    }

    [Fact]
    public void GetStatTotal_Includes_Gems()
    {
        var gear = new Gear();
        gear.Helm.Gems.Add(new Gem() { Strength = 3 });

        gear.GetStatTotal(g => g.Strength).Should().Be(3);
    }
}
