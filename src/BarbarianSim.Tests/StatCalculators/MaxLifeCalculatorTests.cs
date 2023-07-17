using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MaxLifeCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly MaxLifeCalculator _calculator = new();

    [Fact]
    public void Includes_Base_Life()
    {
        _state.Player.BaseLife = 1000;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1000);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.MaxLife = 100;
        _state.Config.Gear.Chest.MaxLife = 100;
        _state.Player.BaseLife = 1000;

        var result = _calculator.Calculate(_state);

        result.Should().Be(1200);
    }

    [Fact]
    public void Includes_Bonus_From_EnhancedChallengingShout()
    {
        _state.Config.Skills.Add(Skill.EnhancedChallengingShout, 1);
        _state.Player.BaseLife = 1000;
        _state.Player.Auras.Add(Aura.ChallengingShout);

        var result = _calculator.Calculate(_state);

        result.Should().Be(1200);
    }
}
