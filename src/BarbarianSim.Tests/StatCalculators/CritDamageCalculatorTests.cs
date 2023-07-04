using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class CritDamageCalculatorTests
{
    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritDamage = 12.0;
        var state = new SimulationState(config);

        var result = CritDamageCalculator.Calculate(state);

        // 1.5 base + 0.12 from helm == 1.62
        result.Should().Be(1.62);
    }

    [Fact]
    public void Base_Crit_Is_50()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);

        var result = CritDamageCalculator.Calculate(state);

        result.Should().Be(1.50);
    }
}
