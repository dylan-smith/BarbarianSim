using BarbarianSim.Config;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DexterityCalculatorTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DexterityCalculator _calculator;

    public DexterityCalculatorTests() => _calculator = new(_mockSimLogger.Object);

    [Fact]
    public void Includes_Base_Value()
    {
        _state.Config.PlayerSettings.Level = 1;

        var result = _calculator.Calculate(_state);

        result.Should().Be(8.0);
    }

    [Fact]
    public void Includes_Dexterity_Gear_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Gear.Helm.Dexterity = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(50.0);
    }

    [Fact]
    public void Includes_All_Stats_Gear_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Gear.Helm.AllStats = 17;

        var result = _calculator.Calculate(_state);

        result.Should().Be(25.0);
    }

    [Fact]
    public void Includes_Dexterity_Paragon_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Paragon.Dexterity = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(50.0);
    }

    [Fact]
    public void Includes_All_Stats_Paragon_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Paragon.AllStats = 17;

        var result = _calculator.Calculate(_state);

        result.Should().Be(25.0);
    }

    [Fact]
    public void Includes_Level_Bonus()
    {
        _state.Config.PlayerSettings.Level = 100;

        var result = _calculator.Calculate(_state);

        result.Should().Be(107.0);
    }
}
