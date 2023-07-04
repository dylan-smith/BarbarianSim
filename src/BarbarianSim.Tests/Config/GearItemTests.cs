using BarbarianSim.Config;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Config;

public class GearItemTests
{
    [Fact]
    public void GetStatWithGems_Without_Gems()
    {
        var gear = new GearItem
        {
            Strength = 7
        };

        gear.GetStatWithGems(x => x.Strength).Should().Be(7);
    }

    [Fact]
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

        gear.GetStatWithGems(x => x.Strength).Should().Be(16);
    }
}
