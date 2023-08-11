using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToSlowedCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageToSlowedCalculator _calculator;

    public DamageToSlowedCalculatorTests() => _calculator = new(_mockSimLogger.Object);

    [Fact]
    public void Includes_DamageToSlowed_When_Enemy_Is_Slowed()
    {
        _state.Config.Gear.Helm.DamageToSlowed = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Slow);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(12.0);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.DamageToSlowed = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Slow);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(12.0);
    }

    [Fact]
    public void Returns_0_When_Enemy_Is_Not_Slowed()
    {
        _state.Config.Gear.Helm.DamageToSlowed = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Stun);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.0);
    }
}
