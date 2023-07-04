using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class WillpowerCalculatorTests
{
    [Fact]
    public void Includes_Base_Value()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        var state = new SimulationState(config);

        var result = WillpowerCalculator.Calculate(state);

        result.Should().Be(7.0);
    }

    [Fact]
    public void Includes_Willpower_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.Willpower = 42;
        var state = new SimulationState(config);

        var result = WillpowerCalculator.Calculate(state);

        result.Should().Be(49.0);
    }

    [Fact]
    public void Includes_All_Stats_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.AllStats = 17;
        var state = new SimulationState(config);

        var result = WillpowerCalculator.Calculate(state);

        result.Should().Be(24.0);
    }

    [Fact]
    public void Includes_Level_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 100;
        var state = new SimulationState(config);

        var result = WillpowerCalculator.Calculate(state);

        result.Should().Be(106.0);
    }
}
