using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class VulnerableDamageBonusCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly VulnerableDamageBonusCalculator _calculator = new();

    [Fact]
    public void Base_Vulnerable_Damage_Is_20()
    {
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_Damage_To_Vulnerable_When_Enemy_Is_Vulnerable()
    {
        _state.Config.Gear.Helm.VulnerableDamage = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().BeApproximately(1.32, 0.000001);
    }

    [Fact]
    public void Returns_1_When_Enemy_Is_Not_Vulnerable()
    {
        _state.Config.Gear.Helm.VulnerableDamage = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Stun);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(1.0);
    }
}
