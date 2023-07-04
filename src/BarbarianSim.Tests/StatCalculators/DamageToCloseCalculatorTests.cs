using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToCloseCalculatorTests
{
    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToClose = 12.0;
        var state = new SimulationState(config);

        var result = DamageToCloseCalculator.Calculate(state);

        result.Should().Be(12);
    }
}
