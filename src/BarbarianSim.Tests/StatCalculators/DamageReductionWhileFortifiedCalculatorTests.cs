using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionWhileFortifiedCalculatorTests
{
    [Fact]
    public void Base_Fortify_DamageReduction_Is_10_Percent()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);
        state.Player.Fortify = 1000;
        state.Player.Life = 800;

        var result = DamageReductionWhileFortifiedCalculator.Calculate(state);

        result.Should().Be(0.9);
    }

    [Fact]
    public void Returns_1_When_Not_Fortified()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);
        state.Player.Fortify = 799;
        state.Player.Life = 800;

        var result = DamageReductionWhileFortifiedCalculator.Calculate(state);

        result.Should().Be(1);
    }

    [Fact]
    public void Returns_1_When_Life_And_Fortify_Are_Equal()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);
        state.Player.Fortify = 1000;
        state.Player.Life = 1000;

        var result = DamageReductionWhileFortifiedCalculator.Calculate(state);

        result.Should().Be(1);
    }

    [Fact]
    public void Multiplies_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageReductionWhileFortified = 12.0;
        config.Gear.Chest.DamageReductionWhileFortified = 12.0;
        var state = new SimulationState(config);
        state.Player.Fortify = 1000;
        state.Player.Life = 800;

        var result = DamageReductionWhileFortifiedCalculator.Calculate(state);

        result.Should().Be(0.69696);
    }
}
