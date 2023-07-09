using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class AggressiveResistanceTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(2, 6)]
    [InlineData(3, 9)]
    [InlineData(4, 9)]
    public void Skill_Points_Determines_DamageReduction(int skillPoints, double damageReduction)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.AggressiveResistance, skillPoints);
        state.Player.Auras.Add(Aura.Berserking);

        AggressiveResistance.GetDamageReduction(state).Should().Be(damageReduction);
    }

    [Fact]
    public void Return_0_If_Not_Specced_In_It()
    {
        var state = new SimulationState(new SimulationConfig());

        AggressiveResistance.GetDamageReduction(state).Should().Be(0);
    }

    [Fact]
    public void Only_Active_While_Berserking()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.AggressiveResistance, 2);

        AggressiveResistance.GetDamageReduction(state).Should().Be(0);
    }
}
