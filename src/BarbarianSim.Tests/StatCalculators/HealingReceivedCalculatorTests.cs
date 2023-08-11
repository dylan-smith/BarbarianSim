using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class HealingReceivedCalculatorTests
{
    private readonly Mock<WillpowerCalculator> _mockWillpowerCalculator = TestHelpers.CreateMock<WillpowerCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly HealingReceivedCalculator _calculator;

    public HealingReceivedCalculatorTests()
    {
        _mockWillpowerCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _calculator = new HealingReceivedCalculator(_mockWillpowerCalculator.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Returns_1_By_Default()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_HealingReceived_Gear_Bonus()
    {
        _state.Config.Gear.Helm.HealingReceived = 20;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_HealingReceived_Paragon_Bonus()
    {
        _state.Config.Paragon.HealingReceived = 20;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_Willpower_Bonus()
    {
        _mockWillpowerCalculator.Setup(x => x.Calculate(_state)).Returns(400.0);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.4);
    }
}
