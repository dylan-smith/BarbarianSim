using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionFromBleedingCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageReductionFromBleedingCalculator _calculator = new();

    [Fact]
    public void Multiplies_DamageReduction_When_Enemy_Is_Bleeding()
    {
        _state.Config.Gear.Helm.DamageReductionFromBleeding = 12.0;
        _state.Config.Gear.Chest.DamageReductionFromBleeding = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Bleeding);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.7744);
    }

    [Fact]
    public void Returns_1_When_Enemy_Is_Not_Bleeding()
    {
        _state.Config.Gear.Helm.DamageReductionFromBleeding = 12.0;

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(1);
    }
}
