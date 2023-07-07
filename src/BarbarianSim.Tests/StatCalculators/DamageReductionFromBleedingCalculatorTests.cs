using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionFromBleedingCalculatorTests
{
    [Fact]
    public void Multiplies_DamageReduction_When_Enemy_Is_Bleeding()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageReductionFromBleeding = 12.0;
        config.Gear.Chest.DamageReductionFromBleeding = 12.0;
        var state = new SimulationState(config);
        state.Enemies.First().Auras.Add(Aura.Bleeding);

        var result = DamageReductionFromBleedingCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.7744);
    }

    [Fact]
    public void Returns_1_When_Enemy_Is_Not_Bleeding()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageReductionFromBleeding = 12.0;
        var state = new SimulationState(config);

        var result = DamageReductionFromBleedingCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(1);
    }
}
