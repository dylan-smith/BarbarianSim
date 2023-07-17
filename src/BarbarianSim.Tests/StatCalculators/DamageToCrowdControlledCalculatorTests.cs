using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToCrowdControlledCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageToCrowdControlledCalculator _calculator = new();

    [Fact]
    public void Includes_Damage_To_Crowd_Controlled_When_Enemy_Is_Slowed()
    {
        _state.Config.Gear.Helm.DamageToCrowdControlled = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Slow);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(12);
    }

    [Fact]
    public void Returns_0_When_Enemy_Is_Not_Crowd_Controlled()
    {
        _state.Config.Gear.Helm.DamageToCrowdControlled = 12.0;

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0);
    }
}
