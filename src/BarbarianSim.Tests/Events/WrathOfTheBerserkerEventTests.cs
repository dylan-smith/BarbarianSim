using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WrathOfTheBerserkerEvenTests
{
    [Fact]
    public void Creates_WrathOfTheBerserkerAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Aura.Should().Be(Aura.WrathOfTheBerserker);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Duration.Should().Be(10);
    }

    [Fact]
    public void Adds_WrathOfTheBerserkerCooldown_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.WrathOfTheBerserkerCooldown);
    }

    [Fact]
    public void Creates_UnstoppableAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent);
        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Aura.Should().Be(Aura.Unstoppable);
        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Duration.Should().Be(5);
    }

    [Fact]
    public void Creates_WrathOfTheBerserkerCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent.Timestamp.Should().Be(183);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent.Aura.Should().Be(Aura.WrathOfTheBerserkerCooldown);
    }

    [Fact]
    public void Creates_BerserkingAuraAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent);
        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Duration.Should().Be(5);
        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Aura.Should().Be(Aura.Berserking);
    }
}
