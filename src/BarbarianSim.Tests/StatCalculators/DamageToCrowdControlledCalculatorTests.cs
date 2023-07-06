using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToCrowdControlledCalculatorTests
{
    [Fact]
    public void Includes_Damage_To_Crowd_Controlled_When_Enemy_Is_Slowed()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToCrowdControlled = 12.0;
        var state = new SimulationState(config);
        state.Enemies.First().Auras.Add(Aura.Slow);

        var result = DamageToCrowdControlledCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(12);
    }

    [Fact]
    public void Returns_0_When_Enemy_Is_Not_Crowd_Controlled()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToCrowdControlled = 12.0;
        var state = new SimulationState(config);

        var result = DamageToCrowdControlledCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0);
    }
}
