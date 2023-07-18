using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class RallyingCryEventHandlerTests
{
    private readonly Mock<BoomingVoice> _mockBoomingVoice = TestHelpers.CreateMock<BoomingVoice>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly RallyingCryEventHandler _handler;

    public RallyingCryEventHandlerTests()
    {
        _mockBoomingVoice.Setup(m => m.GetDurationIncrease(It.IsAny<SimulationState>()))
                         .Returns(1.0);

        _handler = new RallyingCryEventHandler(_mockBoomingVoice.Object);
    }

    [Fact]
    public void Creates_RallyingCryAuraAppliedEvent()
    {
        var rallyingCryEvent = new RallyingCryEvent(123);

        _handler.ProcessEvent(rallyingCryEvent, _state);

        rallyingCryEvent.RallyingCryAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(rallyingCryEvent.RallyingCryAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.RallyingCry);
        rallyingCryEvent.RallyingCryAuraAppliedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.RallyingCryAuraAppliedEvent.Duration.Should().Be(6);
        rallyingCryEvent.RallyingCryAuraAppliedEvent.Aura.Should().Be(Aura.RallyingCry);
    }

    [Fact]
    public void Creates_RallyingCryCooldownCompletedEvent()
    {
        var rallyingCryEvent = new RallyingCryEvent(123);

        _handler.ProcessEvent(rallyingCryEvent, _state);

        rallyingCryEvent.RallyingCryCooldownAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(rallyingCryEvent.RallyingCryCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.RallyingCryCooldown);
        rallyingCryEvent.RallyingCryCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.RallyingCryCooldownAuraAppliedEvent.Aura.Should().Be(Aura.RallyingCryCooldown);
        rallyingCryEvent.RallyingCryCooldownAuraAppliedEvent.Duration.Should().Be(25);
    }

    [Fact]
    public void EnhancedRallyingCry_Creates_UnstoppableAuraAppliedEvent()
    {
        _state.Config.Skills.Add(Skill.EnhancedRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _handler.ProcessEvent(rallyingCryEvent, _state);

        rallyingCryEvent.UnstoppableAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(rallyingCryEvent.UnstoppableAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Unstoppable);
        rallyingCryEvent.UnstoppableAuraAppliedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.UnstoppableAuraAppliedEvent.Aura.Should().Be(Aura.Unstoppable);
        rallyingCryEvent.UnstoppableAuraAppliedEvent.Duration.Should().Be(6);
    }

    [Fact]
    public void TacticalRallyingCry_Creates_FuryGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.TacticalRallyingCry, 1);
        var rallyingCryEvent = new RallyingCryEvent(123);

        _handler.ProcessEvent(rallyingCryEvent, _state);

        rallyingCryEvent.FuryGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(rallyingCryEvent.FuryGeneratedEvent);
        _state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        rallyingCryEvent.FuryGeneratedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.FuryGeneratedEvent.BaseFury.Should().Be(RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY);
    }

    [Fact]
    public void StrategicRallyingCry_Creates_FortifyGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        _state.Player.BaseLife = 4000;
        var rallyingCryEvent = new RallyingCryEvent(123);

        _handler.ProcessEvent(rallyingCryEvent, _state);

        rallyingCryEvent.FortifyGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(rallyingCryEvent.FortifyGeneratedEvent);
        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        rallyingCryEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        rallyingCryEvent.FortifyGeneratedEvent.Amount.Should().Be(400);
    }

    [Fact]
    public void BoomingVoice_Increases_Duration()
    {
        _mockBoomingVoice.Setup(m => m.GetDurationIncrease(_state))
                         .Returns(1.2);

        var rallyingCryEvent = new RallyingCryEvent(123);

        _handler.ProcessEvent(rallyingCryEvent, _state);

        rallyingCryEvent.RallyingCryAuraAppliedEvent.Duration.Should().Be(1.2 * 6.0);
    }
}
