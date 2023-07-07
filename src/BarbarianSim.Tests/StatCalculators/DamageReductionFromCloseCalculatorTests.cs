using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionFromCloseCalculatorTests
{
    [Fact]
    public void Multiplies_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageReductionFromClose = 12.0;
        config.Gear.Chest.DamageReductionFromClose = 12.0;
        var state = new SimulationState(config);

        var result = DamageReductionFromCloseCalculator.Calculate(state);

        result.Should().Be(0.7744);
    }
}
