using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class WillpowerCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly WillpowerCalculator _calculator;

    public WillpowerCalculatorTests() => _calculator = new WillpowerCalculator(_mockSimLogger.Object);

    [Fact]
    public void Includes_Base_Value()
    {
        _state.Config.PlayerSettings.Level = 1;

        var result = _calculator.Calculate(_state);

        result.Should().Be(7.0);
    }

    [Fact]
    public void Includes_Willpower_Gear_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Gear.Helm.Willpower = 42;

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
    public void Includes_Willpower_Paragon_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Paragon.Willpower = 42;

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
