using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class IntelligenceCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly IntelligenceCalculator _calculator;

    public IntelligenceCalculatorTests() => _calculator = new(_mockSimLogger.Object);

    [Fact]
    public void Includes_Base_Value()
    {
        _state.Config.PlayerSettings.Level = 1;

        var result = _calculator.Calculate(_state);

        result.Should().Be(7.0);
    }

    [Fact]
    public void Includes_Intelligence_Gear_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Gear.Helm.Intelligence = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(49.0);
    }

    [Fact]
    public void Includes_All_Stats_Gear_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Gear.Helm.AllStats = 17;

        var result = _calculator.Calculate(_state);

        result.Should().Be(24.0);
    }

    [Fact]
    public void Includes_Intelligence_Paragon_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Paragon.Intelligence = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(49.0);
    }

    [Fact]
    public void Includes_All_Stats_Paragon_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Paragon.AllStats = 17;

        var result = _calculator.Calculate(_state);

        result.Should().Be(24.0);
    }

    [Fact]
    public void Includes_Level_Bonus()
    {
        _state.Config.PlayerSettings.Level = 100;

        var result = _calculator.Calculate(_state);

        result.Should().Be(106.0);
    }
}
