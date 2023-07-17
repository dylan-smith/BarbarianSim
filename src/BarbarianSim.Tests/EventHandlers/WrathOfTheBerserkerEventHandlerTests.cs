using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class WrathOfTheBerserkerEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WrathOfTheBerserkerEventHandler _handler = new();

    [Fact]
    public void Creates_WrathOfTheBerserkerAuraAppliedEvent()
    {
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        _handler.ProcessEvent(wrathOfTheBerserkerEvent, _state);

        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Aura.Should().Be(Aura.WrathOfTheBerserker);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerAuraAppliedEvent.Duration.Should().Be(10);
    }

    [Fact]
    public void Creates_UnstoppableAuraAppliedEvent()
    {
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        _handler.ProcessEvent(wrathOfTheBerserkerEvent, _state);

        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent);
        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Aura.Should().Be(Aura.Unstoppable);
        wrathOfTheBerserkerEvent.UnstoppableAuraAppliedEvent.Duration.Should().Be(5);
    }

    [Fact]
    public void Creates_WrathOfTheBerserkerCooldownAuraAppliedEvent()
    {
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        _handler.ProcessEvent(wrathOfTheBerserkerEvent, _state);

        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WrathOfTheBerserkerCooldown);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownAuraAppliedEvent.Aura.Should().Be(Aura.WrathOfTheBerserkerCooldown);
        wrathOfTheBerserkerEvent.WrathOfTheBerserkerCooldownAuraAppliedEvent.Duration.Should().Be(60);
    }

    [Fact]
    public void Creates_BerserkingAuraAppliedEvent()
    {
        var wrathOfTheBerserkerEvent = new WrathOfTheBerserkerEvent(123);

        _handler.ProcessEvent(wrathOfTheBerserkerEvent, _state);

        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Timestamp.Should().Be(123);
        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Duration.Should().Be(5);
        wrathOfTheBerserkerEvent.BerserkingAuraAppliedEvent.Aura.Should().Be(Aura.Berserking);
    }
}
