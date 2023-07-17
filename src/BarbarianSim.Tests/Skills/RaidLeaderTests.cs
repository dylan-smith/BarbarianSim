using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class RaidLeaderTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly RaidLeader _skill = new();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0.01)]
    [InlineData(2, 0.02)]
    [InlineData(3, 0.03)]
    [InlineData(4, 0.03)]
    public void Skill_Points_Determines_Percent_Heal(int skillPoints, double healPercent)
    {
        _state.Config.Skills.Add(Skill.RaidLeader, skillPoints);

        _skill.GetHealPercentage(_state).Should().Be(healPercent);
    }
}
