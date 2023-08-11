using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToInjuredCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageToInjuredCalculator _calculator;

    public DamageToInjuredCalculatorTests() => _calculator = new(_mockSimLogger.Object);

    [Fact]
    public void Includes_Damage_To_Injured_When_Enemy_Is_Injured()
    {
        _state.Config.Gear.Helm.DamageToInjured = 12.0;
        _state.Enemies.First().Life = 300;
        _state.Enemies.First().MaxLife = 1000;

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(12.0);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.DamageToInjured = 12.0;
        _state.Enemies.First().Life = 300;
        _state.Enemies.First().MaxLife = 1000;

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(12.0);
    }

    [Fact]
    public void Returns_0_When_Enemy_Is_Not_Injured()
    {
        _state.Config.EnemySettings.Life = 1000;
        _state.Config.Gear.Helm.DamageToInjured = 12.0;
        _state.Enemies.First().Life = 1000;

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.0);
    }
}
