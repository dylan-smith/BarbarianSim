using BarbarianSim.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.Config;

[TestClass]
public class GearItemTests
{
    [TestMethod]
    public void GetStatWithGems_Without_Gems()
    {
        var gear = new GearItem
        {
            Strength = 7
        };

        Assert.AreEqual(7, gear.GetStatWithGems(x => x.Strength));
    }

    [TestMethod]
    public void GetStatWithGems_With_Gems()
    {
        var gear = new GearItem
        {
            Strength = 7
        };

        var gem1 = new Gem
        {
            Strength = 3,
        };

        var gem2 = new Gem
        {
            Strength = 6,
        };

        gear.Gems.Add(gem1);
        gear.Gems.Add(gem2);

        Assert.AreEqual(16, gear.GetStatWithGems(x => x.Strength));
    }
}
