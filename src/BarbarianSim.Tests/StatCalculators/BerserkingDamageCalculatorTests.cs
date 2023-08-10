using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class BerserkingDamageCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly BerserkingDamageCalculator _calculator;

    public BerserkingDamageCalculatorTests() => _calculator = new(_mockSimLogger.Object);

    [Fact]
    public void When_Berserking_Returns_1_25()
    {
        _state.Player.Auras.Add(Aura.Berserking);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1.25);
    }

    [Fact]
    public void When_Not_Berserking_Returns_1()
    {
        var result = _calculator.Calculate(_state);

        result.Should().Be(1.0);
    }
}
