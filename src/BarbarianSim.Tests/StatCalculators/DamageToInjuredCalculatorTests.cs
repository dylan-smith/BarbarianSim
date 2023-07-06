using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToInjuredCalculatorTests
{
    [Fact]
    public void Includes_Damage_To_Injured_When_Enemy_Is_Injured()
    {
        var config = new SimulationConfig();
        config.EnemySettings.Life = 1000;
        config.Gear.Helm.DamageToInjured = 12.0;
        var state = new SimulationState(config);
        state.Enemies.First().Life = 300;

        var result = DamageToInjuredCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(12.0);
    }

    [Fact]
    public void Returns_0_When_Enemy_Is_Not_Injured()
    {
        var config = new SimulationConfig();
        config.EnemySettings.Life = 1000;
        config.Gear.Helm.DamageToInjured = 12.0;
        var state = new SimulationState(config);
        state.Enemies.First().Life = 1000;

        var result = DamageToInjuredCalculator.Calculate(state, state.Enemies.First());

        result.Should().Be(0.0);
    }
}
