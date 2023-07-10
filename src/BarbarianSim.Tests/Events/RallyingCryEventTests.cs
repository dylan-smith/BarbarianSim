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
    public void Creates_RallyingCryAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.RallyingCryAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.RallyingCryAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        rallyingCryEvent.RallyingCryAuraAppliedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.RallyingCryAuraAppliedEvent.Duration.Should().Be(6);
        rallyingCryEvent.RallyingCryAuraAppliedEvent.Aura.Should().Be(Aura.RallyingCry);
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
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        rallyingCryEvent.RallyingCryCooldownCompletedEvent.Timestamp.Should().Be(148);
        rallyingCryEvent.RallyingCryCooldownCompletedEvent.Aura.Should().Be(Aura.RallyingCryCooldown);
    }

    [Fact]
    public void EnhancedRallyingCry_Creates_UnstoppableAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.UnstoppableAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.UnstoppableAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Unstoppable);
        rallyingCryEvent.UnstoppableAuraAppliedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.UnstoppableAuraAppliedEvent.Aura.Should().Be(Aura.Unstoppable);
        rallyingCryEvent.UnstoppableAuraAppliedEvent.Duration.Should().Be(6);
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

        rallyingCryEvent.RallyingCryAuraAppliedEvent.Duration.Should().BeApproximately(expectedDuration, 0.0000001);
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
