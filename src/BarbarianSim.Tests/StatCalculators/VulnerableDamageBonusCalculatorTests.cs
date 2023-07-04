using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class VulnerableDamageBonusCalculatorTests
{
    [Fact]
    public void Base_Vulnerable_Damage_Is_20()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemy.Auras.Add(Aura.Vulnerable);

        var result = VulnerableDamageBonusCalculator.Calculate(state);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_Damage_To_Vulnerable_When_Enemy_Is_Vulnerable()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.VulnerableDamage = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Auras.Add(Aura.Vulnerable);

        var result = VulnerableDamageBonusCalculator.Calculate(state);

        result.Should().BeApproximately(1.32, 0.000001);
    }

    [Fact]
    public void Returns_1_When_Enemy_Is_Not_Vulnerable()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.VulnerableDamage = 12.0;
        var state = new SimulationState(config);
        state.Enemy.Auras.Add(Aura.Stun);

        var result = VulnerableDamageBonusCalculator.Calculate(state);

        result.Should().Be(1.0);
    }
}
