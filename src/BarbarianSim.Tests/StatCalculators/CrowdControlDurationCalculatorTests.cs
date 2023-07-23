using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class CrowdControlDurationCalculatorTests
{
    private readonly Mock<SmitingAspect> _mockSmitingAspect = TestHelpers.CreateMock<SmitingAspect>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly CrowdControlDurationCalculator _calculator;

    public CrowdControlDurationCalculatorTests()
    {
        _mockSmitingAspect.Setup(m => m.GetCrowdControlDurationBonus(It.IsAny<SimulationState>())).Returns(1.0);
        _calculator = new(_mockSmitingAspect.Object);
    }

    [Fact]
    public void Includes_SmitingAspect_Bonus()
    {
        _mockSmitingAspect.Setup(m => m.GetCrowdControlDurationBonus(It.IsAny<SimulationState>())).Returns(1.4);
        var result = _calculator.Calculate(_state);

        result.Should().Be(1.4);
    }

    [Fact]
    public void Return_1_When_No_Bonus()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(1.0);
    }
}
