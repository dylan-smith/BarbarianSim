using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class ResistanceToAllCalculatorTests
{
    private readonly Mock<IntelligenceCalculator> _mockIntelligenceCalculator = TestHelpers.CreateMock<IntelligenceCalculator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly ResistanceToAllCalculator _calculator;

    public ResistanceToAllCalculatorTests()
    {
        _mockIntelligenceCalculator.Setup(x => x.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _calculator = new ResistanceToAllCalculator(_mockIntelligenceCalculator.Object);
    }

    [Fact]
    public void Returns_0_By_Default()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(0.0);
    }

    [Fact]
    public void Includes_ResistanceToAll_Gear_Bonus()
    {
        _state.Config.Gear.Helm.ResistanceToAll = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.42);
    }

    [Fact]
    public void Includes_Intelligence_Bonus()
    {
        _mockIntelligenceCalculator.Setup(x => x.Calculate(_state)).Returns(400.0);

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.2);
    }
}
