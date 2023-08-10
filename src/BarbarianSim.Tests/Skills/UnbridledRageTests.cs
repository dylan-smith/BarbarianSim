using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class UnbridledRageTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly UnbridledRage _skill;

    public UnbridledRageTests() => _skill = new(_mockSimLogger.Object);

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

    [Fact]
    public void GetDamageBonus_When_Active()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _skill.GetDamageBonus(_state, SkillType.Core);

        result.Should().Be(2.35);
    }

    [Fact]
    public void GetDamageBonus_Return_1_When_Not_Skilled()
    {
        var result = _skill.GetDamageBonus(_state, SkillType.Core);

        result.Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Return_1_When_Not_Non_Core_Skill()
    {
        _state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = _skill.GetDamageBonus(_state, SkillType.Basic);

        result.Should().Be(1.0);
    }
}
