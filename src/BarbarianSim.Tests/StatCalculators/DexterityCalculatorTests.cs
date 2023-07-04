using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DexterityCalculatorTests
{
    [Fact]
    public void Includes_Base_Value()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        result.Should().Be(8.0);
    }

    [Fact]
    public void Includes_Dexterity_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.Dexterity = 42;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        result.Should().Be(50.0);
    }

    [Fact]
    public void Includes_All_Stats_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 1;
        config.Gear.Helm.AllStats = 17;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        result.Should().Be(25.0);
    }

    [Fact]
    public void Includes_Level_Bonus()
    {
        var config = new SimulationConfig();
        config.PlayerSettings.Level = 100;
        var state = new SimulationState(config);

        var result = DexterityCalculator.Calculate(state);

        result.Should().Be(107.0);
    }
}
