using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WrathOfTheBerserkerEvenTests
{
    [Fact]
    public void Adds_WrathOfTheBerserker_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.WrathOfTheBerserker);
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
    public void Adds_Unstoppable_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Unstoppable);
    }

    [Fact]
    public void Creates_WrathOfTheBerserkerCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is CooldownCompletedEvent);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent.Timestamp.Should().Be(183);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownCompletedEvent.Aura.Should().Be(Aura.WrathOfTheBerserkerCooldown);
    }

    [Fact]
    public void Creates_WrathOfTheBerserkerExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.WrathOfTheBerserkerExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.WrathOfTheBerserkerExpiredEvent);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerExpiredEvent.Timestamp.Should().Be(133);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerExpiredEvent.Aura.Should().Be(Aura.WrathOfTheBerserker);
    }

    [Fact]
    public void Creates_UnstoppableExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.UnstoppableExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.UnstoppableExpiredEvent);
        wrathOfTheBerserkerEvent.UnstoppableExpiredEvent.Timestamp.Should().Be(128);
        wrathOfTheBerserkerEvent.UnstoppableExpiredEvent.Aura.Should().Be(Aura.Unstoppable);
    }

    [Fact]
    public void Creates_BerserkingAppliedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        wrathOfTheBerserkerEvent.ProcessEvent(state);

        wrathOfTheBerserkerEvent.BerserkingAppliedEvent.Should().NotBeNull();
        state.Events.Should().Contain(wrathOfTheBerserkerEvent.BerserkingAppliedEvent);
        state.Events.Should().ContainSingle(e => e is CooldownCompletedEvent);
        wrathOfTheBerserkerEvent.BerserkingAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.BerserkingAppliedEvent.Duration.Should().Be(5);
    }
}
