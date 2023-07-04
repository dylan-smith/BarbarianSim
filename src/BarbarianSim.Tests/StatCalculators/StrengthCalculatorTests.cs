using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class StrengthCalculatorTests
{
    [Fact]
    public void Includes_Base_Value()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        result.Should().Be(10.0);
    }

    [Fact]
    public void Includes_Strength_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.Strength = 42;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        result.Should().Be(52.0);
    }

    [Fact]
    public void Includes_All_Stats_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.AllStats = 17;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        result.Should().Be(27.0);
    }

    [Fact]
    public void Includes_Level_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 100;
        var state = new SimulationState(config);

        var result = StrengthCalculator.Calculate(state);

        result.Should().Be(109.0);
    }
}
