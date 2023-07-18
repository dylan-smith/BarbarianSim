using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class UnbridledRageTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly UnbridledRage _skill = new();

    [Fact]
    public void Double_The_Cost_With_Unbridled_Rage()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _skill.GetFuryCostReduction(_state, SkillType.Core);

        result.Should().Be(2.0);
    }

    [Fact]
    public void Unbridled_Rage_Only_Doubles_Core_Skills()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _skill.GetFuryCostReduction(_state, SkillType.Basic);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Returns_1_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 0);

        var result = _skill.GetFuryCostReduction(_state, SkillType.Core);

        result.Should().Be(1.0);
    }
}
