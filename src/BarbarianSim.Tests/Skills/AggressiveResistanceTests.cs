using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class AggressiveResistanceTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly AggressiveResistance _skill = new();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(2, 6)]
    [InlineData(3, 9)]
    [InlineData(4, 9)]
    public void Skill_Points_Determines_DamageReduction(int skillPoints, double damageReduction)
    {
        _state.Config.Skills.Add(Skill.AggressiveResistance, skillPoints);
        _state.Player.Auras.Add(Aura.Berserking);

        _skill.GetDamageReduction(_state).Should().Be(damageReduction);
    }

    [Fact]
    public void Return_0_If_Not_Specced_In_It()
    {
        _state.Player.Auras.Add(Aura.Berserking);
        _skill.GetDamageReduction(_state).Should().Be(0);
    }

    [Fact]
    public void Only_Active_While_Berserking()
    {
        _state.Config.Skills.Add(Skill.AggressiveResistance, 2);

        _skill.GetDamageReduction(_state).Should().Be(0);
    }
}
