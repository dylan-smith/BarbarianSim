using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class AttackSpeedCalculatorTests
{
    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.AttackSpeed = 60.0;
        var state = new SimulationState(config);

        var result = AttackSpeedCalculator.Calculate(state);

        result.Should().Be(0.625); // 1 / 1.60 == 0.625
    }
}
