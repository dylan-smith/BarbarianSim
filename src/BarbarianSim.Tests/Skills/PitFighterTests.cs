using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class PitFighterTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1.03)]
    [InlineData(2, 1.06)]
    [InlineData(3, 1.09)]
    [InlineData(4, 1.09)]
    public void Skill_Points_Determines_CloseDamageBonus(int skillPoints, double damageBonus)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.PitFighter, skillPoints);

        PitFighter.GetCloseDamageBonus(state).Should().Be(damageBonus);
    }
}
