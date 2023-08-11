using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionFromCloseCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageReductionFromCloseCalculator _calculator;

    public DamageReductionFromCloseCalculatorTests() => _calculator = new(_mockSimLogger.Object);

    [Fact]
    public void Multiplies_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.DamageReductionFromClose = 12.0;
        _state.Config.Gear.Chest.DamageReductionFromClose = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.7744);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.DamageReductionFromClose = 12.0;
        _state.Config.Gear.Chest.DamageReductionFromClose = 12.0;

        var result = _calculator.Calculate(_state);

        result.Should().Be(0.7744);
    }
}
