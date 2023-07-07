using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests;

public class PlayerStateTests
{
    [Fact]
    public void IsFortified_Returns_True_When_Fortify_Greater_Than_Life()
    {
        var playerState = new PlayerState
        {
            Fortify = 800,
            Life = 799,
        };

        playerState.IsFortified().Should().BeTrue();
    }

    [Fact]
    public void IsFortified_Returns_False_When_Fortify_Less_Than_Life()
    {
        var playerState = new PlayerState
        {
            Fortify = 999,
            Life = 1000,
        };

        playerState.IsFortified().Should().BeFalse();
    }

    [Fact]
    public void IsFortified_Returns_False_When_Fortify_Equals_MaxLife()
    {
        var playerState = new PlayerState
        {
            Fortify = 1000,
            Life = 1000,
        };

        playerState.IsFortified().Should().BeFalse();
    }

    [Fact]
    public void IsInjured_Returns_True_When_Life_Less_Than_35_Percent()
    {
        var playerState = new PlayerState
        {
            Life = 34,
        };

        playerState.IsInjured(100).Should().BeTrue();
    }

    [Fact]
    public void IsInjured_Returns_False_When_Life_Greater_Than_35_Percent()
    {
        var playerState = new PlayerState
        {
            Life = 36,
        };

        playerState.IsInjured(100).Should().BeFalse();
    }

    [Fact]
    public void IsHealthy_Returns_True_When_Life_Greater_Than_80_Percent()
    {
        var playerState = new PlayerState
        {
            Life = 801,
        };

        playerState.IsHealthy(1000).Should().BeTrue();
    }

    [Fact]
    public void IsHealthy_Returns_False_When_Life_Less_Than_80_Percent()
    {
        var playerState = new PlayerState
        {
            Life = 799,
        };

        playerState.IsHealthy(1000).Should().BeFalse();
    }
}
