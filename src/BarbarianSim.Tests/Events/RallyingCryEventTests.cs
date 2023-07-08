using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class RallyingCryEventTests
{
    [Fact]
    public void Adds_RallyingCry_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.RallyingCry);
    }

    [Fact]
    public void Adds_RallyingCryCooldown_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.RallyingCryCooldown);
    }

    [Fact]
    public void Creates_RallyingCryCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.RallyingCryCooldownCompletedEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.RallyingCryCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is RallyingCryCooldownCompletedEvent);
        rallyingCryEvent.RallyingCryCooldownCompletedEvent.Timestamp.Should().Be(148);
    }

    [Fact]
    public void Creates_RallyingCryExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.RallyingCryExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.RallyingCryExpiredEvent);
        state.Events.Should().ContainSingle(e => e is RallyingCryExpiredEvent);
        rallyingCryEvent.RallyingCryExpiredEvent.Timestamp.Should().Be(129);
    }

    [Fact]
    public void EnhancedRallyingCry_Adds_Unstoppable_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Unstoppable);
    }

    [Fact]
    public void EnhancedRallyingCry_Creates_UnstoppableExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.UnstoppableExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.UnstoppableExpiredEvent);
        state.Events.Should().ContainSingle(e => e is UnstoppableExpiredEvent);
        rallyingCryEvent.UnstoppableExpiredEvent.Timestamp.Should().Be(129);
    }

    [Fact]
    public void TacticalRallyingCry_Creates_FuryGeneratedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.TacticalRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.FuryGeneratedEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.FuryGeneratedEvent);
        state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        rallyingCryEvent.FuryGeneratedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.FuryGeneratedEvent.BaseFury.Should().Be(RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY);
    }

    [Fact]
    public void StrategicRallyingCry_Creates_FortifyGeneratedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        state.Player.BaseLife = 4000;
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.FortifyGeneratedEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.FortifyGeneratedEvent);
        state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        rallyingCryEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.FortifyGeneratedEvent.Amount.Should().Be(400);
    }

    [Theory]
    [InlineData(0, 6)]
    [InlineData(1, 6.48)]
    [InlineData(2, 6.96)]
    [InlineData(3, 7.44)]
    [InlineData(4, 7.44)]
    public void BoomingVoice_Increases_Duration(int skillPoints, double expectedDuration)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.BoomingVoice, skillPoints);

        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.RallyingCryExpiredEvent.Timestamp.Should().Be(123 + expectedDuration);
    }

    [Fact]
    public void Creates_RaidLeaderProcEvent()
    {
        var config = new SimulationConfig();
        config.Skills.Add(Skill.RaidLeader, 1);
        config.Skills.Add(Skill.BoomingVoice, 2);
        var state = new SimulationState(config);
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.RaidLeaderProcEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.RaidLeaderProcEvent);
        state.Events.Should().ContainSingle(e => e is RaidLeaderProcEvent);
        rallyingCryEvent.RaidLeaderProcEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.RaidLeaderProcEvent.Duration.Should().BeApproximately(6.96, 0.000000001);
    }
}
