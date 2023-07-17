using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionWhileFortifiedCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageReductionWhileFortifiedCalculator _calculator = new();

    [Fact]
    public void Base_Fortify_DamageReduction_Is_10_Percent()
    {
        _state.Player.Fortify = 1000;
        _state.Player.Life = 800;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.9);
    }

    [Fact]
    public void Returns_1_When_Not_Fortified()
    {
        _state.Player.Fortify = 799;
        _state.Player.Life = 800;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1);
    }

    [Fact]
    public void Returns_1_When_Life_And_Fortify_Are_Equal()
    {
        _state.Player.Fortify = 1000;
        _state.Player.Life = 1000;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1);
    }

    [Fact]
    public void Multiplies_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.DamageReductionWhileFortified = 12.0;
        _state.Config.Gear.Chest.DamageReductionWhileFortified = 12.0;
        _state.Player.Fortify = 1000;
        _state.Player.Life = 800;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.69696);
    }
}
