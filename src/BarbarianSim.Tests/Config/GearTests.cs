using BarbarianSim.Config;
using BarbarianSim.Gems;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.Config;

[TestClass]
public class GearTests
{
    [TestMethod]
    public void AllGear_Returns_12_Items()
    {
        var gear = new Gear();
        Assert.AreEqual(12, gear.AllGear.Count());
    }

    [TestMethod]
    public void GetAllGems_Returns_0_Gems()
    {
        var gear = new Gear();
        Assert.AreEqual(0, gear.GetAllGems().Count());
    }

    [TestMethod]
    public void GetAllGems_Returns_All_Gems()
    {
        var gear = new Gear();
        gear.Helm.Gems.Add(new RoyalSapphire());
        gear.Helm.Gems.Add(new RoyalSapphire());
        gear.Chest.Gems.Add(new RoyalSapphire());
        gear.Amulet.Gems.Add(new RoyalSapphire());

        Assert.AreEqual(4, gear.GetAllGems().Count());
    }

    [TestMethod]
    public void GetStatTotal_Returns_0()
    {
        var gear = new Gear();
        Assert.AreEqual(0, gear.GetStatTotal(g => g.Strength));
    }

    [TestMethod]
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

        Assert.AreEqual(12, gear.GetStatTotal(g => g.Strength));
    }

    [TestMethod]
    public void GetStatTotal_Includes_Gems()
    {
        var gear = new Gear();
        gear.Helm.Gems.Add(new Gem() { Strength = 3 });

        Assert.AreEqual(3, gear.GetStatTotal(g => g.Strength));
    }
}
