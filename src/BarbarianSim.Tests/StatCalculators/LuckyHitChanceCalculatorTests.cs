using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class LuckyHitChanceCalculatorTests
{
    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.LuckyHitChance = 12.0;
        var state = new SimulationState(config);

        var result = LuckyHitChanceCalculator.Calculate(state);

        result.Should().Be(0.12);
    }
}
