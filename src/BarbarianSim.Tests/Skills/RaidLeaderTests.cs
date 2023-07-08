using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class RaidLeaderTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0.01)]
    [InlineData(2, 0.02)]
    [InlineData(3, 0.03)]
    [InlineData(4, 0.03)]
    public void Skill_Points_Determines_Percent_Heal(int skillPoints, double healPercent)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.RaidLeader, skillPoints);

        RaidLeader.GetHealPercentage(state).Should().Be(healPercent);
    }
}
