using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class ChallengingShoutEventTests
{
    [Fact]
    public void Adds_ChallengingShout_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.ChallengingShout);
    }

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
        state.Events.Should().ContainSingle(e => e is ChallengingShoutCooldownCompletedEvent);
        challengingShoutEvent.ChallengingShoutCooldownCompletedEvent.Timestamp.Should().Be(148);
    }

    [Fact]
    public void Creates_ChallengingShoutExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        challengingShoutEvent.ChallengingShoutExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(challengingShoutEvent.ChallengingShoutExpiredEvent);
        state.Events.Should().ContainSingle(e => e is ChallengingShoutExpiredEvent);
        challengingShoutEvent.ChallengingShoutExpiredEvent.Timestamp.Should().Be(129);
    }

    [Fact]
    public void Adds_Taunt_Aura_To_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        challengingShoutEvent.ProcessEvent(state);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Should().Contain(Aura.Taunt);
        }
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

        challengingShoutEvent.ChallengingShoutExpiredEvent.Timestamp.Should().Be(123 + expectedDuration);
    }
}
