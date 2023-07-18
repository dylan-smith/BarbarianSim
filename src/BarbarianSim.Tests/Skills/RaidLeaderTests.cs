using System.Reflection.Metadata;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
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

    [Fact]
    public void Creates_RaidLeaderProcEvent()
    {
        _state.Config.Skills.Add(Skill.RaidLeader, 1);
        var challengingShoutEvent = new ChallengingShoutEvent(123)
        {
            Duration = 12.2
        };

        _skill.ProcessEvent(challengingShoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is RaidLeaderProcEvent);
        _state.Events.OfType<RaidLeaderProcEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<RaidLeaderProcEvent>().First().Duration.Should().Be(12.2);
    }

    [Fact]
    public void Only_Creates_RaidLeaderProcEvent_If_Skilled()
    {
        _state.Config.Skills.Add(Skill.RaidLeader, 0);
        var challengingShoutEvent = new ChallengingShoutEvent(123)
        {
            Duration = 12.2
        };

        _skill.ProcessEvent(challengingShoutEvent, _state);

        _state.Events.Should().NotContain(e => e is RaidLeaderProcEvent);
    }
}
