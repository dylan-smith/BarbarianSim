using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MaxLifeCalculatorTests
{
    [Fact]
    public void Includes_Base_Life()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.BaseLife = 1000;

        var result = MaxLifeCalculator.Calculate(state);

        result.Should().Be(1000);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.MaxLife = 100;
        config.Gear.Chest.MaxLife = 100;
        var state = new SimulationState(config);
        state.Player.BaseLife = 1000;

        var result = MaxLifeCalculator.Calculate(state);

        result.Should().Be(1200);
    }

    [Fact]
    public void Includes_Bonus_From_EnhancedChallengingShout()
    {
        var config = new SimulationConfig();
        config.Skills.Add(Skill.EnhancedChallengingShout, 1);
        var state = new SimulationState(config);
        state.Player.BaseLife = 1000;
        state.Player.Auras.Add(Aura.ChallengingShout);

        var result = MaxLifeCalculator.Calculate(state);

        result.Should().Be(1200);
    }
}
