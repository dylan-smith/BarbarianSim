using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class StrengthCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly StrengthCalculator _calculator = new();

    [Fact]
    public void Includes_Base_Value()
    {
        _state.Config.PlayerSettings.Level = 1;

        var result = _calculator.Calculate(_state);

        result.Should().Be(10.0);
    }

    [Fact]
    public void Includes_Strength_Gear_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Gear.Helm.Strength = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(52.0);
    }

    [Fact]
    public void Includes_All_Stats_Gear_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Gear.Helm.AllStats = 17;

        var result = _calculator.Calculate(_state);

        result.Should().Be(27.0);
    }

    [Fact]
    public void Includes_Strength_Paragon_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Paragon.Strength = 42;

        var result = _calculator.Calculate(_state);

        result.Should().Be(52.0);
    }

    [Fact]
    public void Includes_All_Stats_Paragon_Bonus()
    {
        _state.Config.PlayerSettings.Level = 1;
        _state.Config.Paragon.AllStats = 17;

        var result = _calculator.Calculate(_state);

        result.Should().Be(27.0);
    }

    [Fact]
    public void Includes_Level_Bonus()
    {
        _state.Config.PlayerSettings.Level = 100;

        var result = _calculator.Calculate(_state);

        result.Should().Be(109.0);
    }

    [Fact]
    public void GetStrengthMultiplier_Returns_Correct_Value()
    {
        _state.Config.Gear.Helm.Strength = 42;
        _state.Config.PlayerSettings.Level = 1;
        var result = _calculator.GetStrengthMultiplier(_state, SkillType.Core);

        result.Should().Be(1.052);
    }

    [Fact]
    public void GetStrengthMultiplier_Returns_0_When_Not_Skill_Damage()
    {
        _state.Config.Gear.Helm.Strength = 42;
        _state.Config.PlayerSettings.Level = 1;
        var result = _calculator.GetStrengthMultiplier(_state, SkillType.None);

        result.Should().Be(1.0);
    }
}
