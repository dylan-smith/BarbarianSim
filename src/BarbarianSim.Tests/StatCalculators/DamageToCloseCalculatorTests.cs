using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageToCloseCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageToCloseCalculator _calculator;

    public DamageToCloseCalculatorTests() => _calculator = new(_mockSimLogger.Object);

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.DamageToClose = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(12);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.DamageToClose = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(12);
    }
}
