﻿using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class RaidLeaderTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly RaidLeader _skill;

    public RaidLeaderTests() => _skill = new(_mockSimLogger.Object);

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
    public void ChallengingShoutEvent_Creates_RaidLeaderProcEvent()
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

    [Fact]
    public void RallyingCryEvent_Creates_RaidLeaderProcEvent()
    {
        _state.Config.Skills.Add(Skill.RaidLeader, 1);
        var rallyingCryEvent = new RallyingCryEvent(123)
        {
            Duration = 12.2
        };

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().ContainSingle(e => e is RaidLeaderProcEvent);
        _state.Events.OfType<RaidLeaderProcEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<RaidLeaderProcEvent>().First().Duration.Should().Be(12.2);
    }

    [Fact]
    public void WarCryEvent_Creates_RaidLeaderProcEvent()
    {
        _state.Config.Skills.Add(Skill.RaidLeader, 1);
        var warCryEvent = new WarCryEvent(123)
        {
            Duration = 12.2
        };

        _skill.ProcessEvent(warCryEvent, _state);

        _state.Events.Should().ContainSingle(e => e is RaidLeaderProcEvent);
        _state.Events.OfType<RaidLeaderProcEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<RaidLeaderProcEvent>().First().Duration.Should().Be(12.2);
    }
}
