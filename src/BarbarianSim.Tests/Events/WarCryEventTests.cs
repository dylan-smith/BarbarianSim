using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WarCryEventTests
{
    [Fact]
    public void Adds_WarCry_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.WarCry);
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
        state.Events.Should().ContainSingle(e => e is CooldownCompletedEvent);
        warCryEvent.WarCryCooldownCompletedEvent.Timestamp.Should().Be(148);
    }

    [Fact]
    public void Creates_AuraExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.WarCryExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(warCryEvent.WarCryExpiredEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        warCryEvent.WarCryExpiredEvent.Timestamp.Should().Be(129);
    }

    [Fact]
    public void EnhancedWarCry_Creates_BerserkingAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedWarCry, 1);
        var warCryEvent = new WarCryEvent(123);

        warCryEvent.ProcessEvent(state);

        warCryEvent.BerserkingAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(warCryEvent.BerserkingAppliedEvent);
        state.Events.Should().ContainSingle(e => e is BerserkingAppliedEvent);
        warCryEvent.BerserkingAppliedEvent.Timestamp.Should().Be(123);
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

        warCryEvent.WarCryExpiredEvent.Timestamp.Should().Be(123 + expectedDuration);
    }
}
