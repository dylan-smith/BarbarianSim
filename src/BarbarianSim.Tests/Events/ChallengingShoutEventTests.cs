using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class ChallengingShoutEventTests
{
    [Fact]
    public void Adds_ChallengingShoutCooldown_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.ChallengingShoutCooldown);
    }

    [Fact]
    public void Creates_ChallengingShoutCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        challengingShoutEvent.ChallengingShoutCooldownCompletedEvent.Should().NotBeNull();
        state.Events.Should().Contain(challengingShoutEvent.ChallengingShoutCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is CooldownCompletedEvent);
        challengingShoutEvent.ChallengingShoutCooldownCompletedEvent.Timestamp.Should().Be(148);
        challengingShoutEvent.ChallengingShoutCooldownCompletedEvent.Aura.Should().Be(Aura.ChallengingShoutCooldown);
    }

    [Fact]
    public void Creates_ChallengingShoutAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        challengingShoutEvent.ChallengingShoutAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(challengingShoutEvent.ChallengingShoutAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ChallengingShout);
        challengingShoutEvent.ChallengingShoutAuraAppliedEvent.Timestamp.Should().Be(123);
        challengingShoutEvent.ChallengingShoutAuraAppliedEvent.Aura.Should().Be(Aura.ChallengingShout);
        challengingShoutEvent.ChallengingShoutAuraAppliedEvent.Duration.Should().Be(6);
    }

    [Fact]
    public void Creates_TauntAuraAppliedEvents()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        challengingShoutEvent.TauntAuraAppliedEvent.Should().HaveCount(3);
        state.Events.Count(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Should().Be(3);
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().All(e => e.Timestamp == 123).Should().BeTrue();
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().All(e => e.Duration == 6).Should().BeTrue();
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().First().Target.Should().Be(state.Enemies.First());
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().Last().Target.Should().Be(state.Enemies.Last());
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

        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        challengingShoutEvent.ChallengingShoutAuraAppliedEvent.Duration.Should().BeApproximately(expectedDuration, 0.000001);
    }

    [Fact]
    public void Creates_RaidLeaderProcEvent()
    {
        var config = new SimulationConfig();
        config.Skills.Add(Skill.RaidLeader, 1);
        config.Skills.Add(Skill.BoomingVoice, 2);
        var state = new SimulationState(config);
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        challengingShoutEvent.RaidLeaderProcEvent.Should().NotBeNull();
        state.Events.Should().Contain(challengingShoutEvent.RaidLeaderProcEvent);
        state.Events.Should().ContainSingle(e => e is RaidLeaderProcEvent);
        challengingShoutEvent.RaidLeaderProcEvent.Timestamp.Should().Be(123);
        challengingShoutEvent.RaidLeaderProcEvent.Duration.Should().BeApproximately(6.96, 0.000000001);
    }
}
