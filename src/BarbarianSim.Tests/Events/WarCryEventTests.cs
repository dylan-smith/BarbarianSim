using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WarCryEventTests
{
    [Fact]
    public void Creates_WarCryAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.WarCryAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(warCryEvent.WarCryAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WarCry);
        warCryEvent.WarCryAuraAppliedEvent.Timestamp.Should().Be(123);
        warCryEvent.WarCryAuraAppliedEvent.Aura.Should().Be(Aura.WarCry);
        warCryEvent.WarCryAuraAppliedEvent.Duration.Should().Be(6);
    }

    [Fact]
    public void Adds_WarCryCooldown_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.WarCryCooldown);
    }

    [Fact]
    public void Creates_CooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.WarCryCooldownCompletedEvent.Should().NotBeNull();
        state.Events.Should().Contain(warCryEvent.WarCryCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        warCryEvent.WarCryCooldownCompletedEvent.Timestamp.Should().Be(148);
    }

    [Fact]
    public void EnhancedWarCry_Creates_BerserkingAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedWarCry, 1);
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.BerserkingAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(warCryEvent.BerserkingAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        warCryEvent.BerserkingAuraAppliedEvent.Timestamp.Should().Be(123);
        warCryEvent.BerserkingAuraAppliedEvent.Duration.Should().Be(4.0);
        warCryEvent.BerserkingAuraAppliedEvent.Aura.Should().Be(Aura.Berserking);
    }

    [Fact]
    public void MightyWarCry_Creates_FortifyGeneratedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.MightyWarCry, 1);
        state.Player.BaseLife = 4000;
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.FortifyGeneratedEvent.Should().NotBeNull();
        state.Events.Should().Contain(warCryEvent.FortifyGeneratedEvent);
        state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        warCryEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        warCryEvent.FortifyGeneratedEvent.Amount.Should().Be(600);
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

        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.WarCryAuraAppliedEvent.Duration.Should().BeApproximately(expectedDuration, 0.0000001);
    }

    [Fact]
    public void Creates_RaidLeaderProcEvent()
    {
        var config = new SimulationConfig();
        config.Skills.Add(Skill.RaidLeader, 1);
        config.Skills.Add(Skill.BoomingVoice, 2);
        var state = new SimulationState(config);
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.RaidLeaderProcEvent.Should().NotBeNull();
        state.Events.Should().Contain(warCryEvent.RaidLeaderProcEvent);
        state.Events.Should().ContainSingle(e => e is RaidLeaderProcEvent);
        warCryEvent.RaidLeaderProcEvent.Timestamp.Should().Be(123);
        warCryEvent.RaidLeaderProcEvent.Duration.Should().BeApproximately(6.96, 0.000000001);
    }
}
