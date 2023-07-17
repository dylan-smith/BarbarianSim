using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class FuryCostReductionCalculatorTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly FuryCostReductionCalculator _calculator = new();

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.FuryCostReduction = 12.0;

        var result = _calculator.Calculate(_state, SkillType.Basic);

        result.Should().Be(0.88);
    }

    [Fact]
    public void Double_The_Cost_With_Unbridled_Rage()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _calculator.Calculate(_state, SkillType.Core);

        result.Should().Be(2.0);
    }

    [Fact]
    public void Unbridled_Rage_Only_Doubles_Core_Skills()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _calculator.Calculate(_state, SkillType.Basic);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Unbridled_Rage_Multiplies_Properly_With_Other_Bonuses()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);
        _state.Config.Gear.Helm.FuryCostReduction = 12.0;

        var result = _calculator.Calculate(_state, SkillType.Core);

        result.Should().Be(1.76);
    }
}
