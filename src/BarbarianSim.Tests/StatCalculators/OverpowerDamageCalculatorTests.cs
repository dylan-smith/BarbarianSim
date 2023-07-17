using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class OverpowerDamageCalculatorTests
{
    private readonly Mock<WillpowerCalculator> _mockWillpowerCalculator = TestHelpers.CreateMock<WillpowerCalculator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly OverpowerDamageCalculator _calculator;

    public OverpowerDamageCalculatorTests()
    {
        _mockWillpowerCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _calculator = new OverpowerDamageCalculator(_mockWillpowerCalculator.Object);
    }

    [Fact]
    public void Returns_1_By_Default()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_OverpowerDamage_Gear_Bonus()
    {
        _state.Config.Gear.Helm.OverpowerDamage = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.42);
    }

    [Fact]
    public void Includes_Willpower_Bonus()
    {
        _mockWillpowerCalculator.Setup(x => x.Calculate(_state)).Returns(400.0);

        var result = _calculator.Calculate(_state);

        result.Should().Be(2.0);
    }
}
