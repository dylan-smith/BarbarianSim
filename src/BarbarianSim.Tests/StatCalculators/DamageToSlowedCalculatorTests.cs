using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToSlowedCalculatorTests
{
    [Fact]
    public void Includes_Damage_To_Slowed_When_Enemy_Is_Slowed()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToSlowed = 12.0;
        var state = new SimulationState(config);
        state.Enemies.First().Auras.Add(Aura.Slow);

        var result = DamageToSlowedCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(12.0);
    }

    [Fact]
    public void Returns_0_When_Enemy_Is_Not_Slowed()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.DamageToSlowed = 12.0;
        var state = new SimulationState(config);
        state.Enemies.First().Auras.Add(Aura.Stun);

        var result = DamageToSlowedCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.0);
    }
}
