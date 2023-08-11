using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionWhileInjuredCalculatorTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageReductionWhileInjuredCalculator _calculator;

    public DamageReductionWhileInjuredCalculatorTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1000.0);
        _calculator = new DamageReductionWhileInjuredCalculator(_mockMaxLifeCalculator.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Returns_1_When_Not_Injured()
    {
        _state.Player.Life = 800;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1);
    }

    [Fact]
    public void Multiplies_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.DamageReductionWhileInjured = 12.0;
        _state.Config.Gear.Chest.DamageReductionWhileInjured = 12.0;
        _state.Player.Life = 300;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.7744);
    }

    [Fact]
    public void Multiplies_Stats_From_Paragon()
    {
        _state.Config.Paragon.DamageReductionWhileInjured = 12.0;
        _state.Config.Gear.Chest.DamageReductionWhileInjured = 12.0;
        _state.Player.Life = 300;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.7744);
    }
}
