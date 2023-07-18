using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class WarCryEventHandlerTests
{
    private readonly Mock<BoomingVoice> _mockBoomingVoice = TestHelpers.CreateMock<BoomingVoice>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WarCryEventHandler _handler;

    public WarCryEventHandlerTests()
    {
        _mockBoomingVoice.Setup(m => m.GetDurationIncrease(It.IsAny<SimulationState>()))
                         .Returns(1.0);

        _handler = new WarCryEventHandler(_mockBoomingVoice.Object);
    }

    [Fact]
    public void Creates_WarCryAuraAppliedEvent()
    {
        var warCryEvent = new WarCryEvent(123);

        _handler.ProcessEvent(warCryEvent, _state);

        warCryEvent.WarCryAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(warCryEvent.WarCryAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WarCry);
        warCryEvent.WarCryAuraAppliedEvent.Timestamp.Should().Be(123);
        warCryEvent.WarCryAuraAppliedEvent.Aura.Should().Be(Aura.WarCry);
        warCryEvent.WarCryAuraAppliedEvent.Duration.Should().Be(6);
    }

    [Fact]
    public void Creates_WarCryCooldownAuraAppliedEvent()
    {
        var warCryEvent = new WarCryEvent(123);

        _handler.ProcessEvent(warCryEvent, _state);

        warCryEvent.WarCryCooldownAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(warCryEvent.WarCryCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.WarCryCooldown);
        warCryEvent.WarCryCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        warCryEvent.WarCryCooldownAuraAppliedEvent.Aura.Should().Be(Aura.WarCryCooldown);
        warCryEvent.WarCryCooldownAuraAppliedEvent.Duration.Should().Be(25);
    }

    [Fact]
    public void EnhancedWarCry_Creates_BerserkingAuraAppliedEvent()
    {
        _state.Config.Skills.Add(Skill.EnhancedWarCry, 1);
        var warCryEvent = new WarCryEvent(123);

        _handler.ProcessEvent(warCryEvent, _state);

        warCryEvent.BerserkingAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(warCryEvent.BerserkingAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        warCryEvent.BerserkingAuraAppliedEvent.Timestamp.Should().Be(123);
        warCryEvent.BerserkingAuraAppliedEvent.Duration.Should().Be(4.0);
        warCryEvent.BerserkingAuraAppliedEvent.Aura.Should().Be(Aura.Berserking);
    }

    [Fact]
    public void MightyWarCry_Creates_FortifyGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.MightyWarCry, 1);
        _state.Player.BaseLife = 4000;
        var warCryEvent = new WarCryEvent(123);

        _handler.ProcessEvent(warCryEvent, _state);

        warCryEvent.FortifyGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(warCryEvent.FortifyGeneratedEvent);
        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        warCryEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        warCryEvent.FortifyGeneratedEvent.Amount.Should().Be(600);
    }

    [Fact]
    public void BoomingVoice_Increases_Duration()
    {
        _mockBoomingVoice.Setup(m => m.GetDurationIncrease(_state))
                         .Returns(1.2);

        var warCryEvent = new WarCryEvent(123);

        _handler.ProcessEvent(warCryEvent, _state);

        warCryEvent.WarCryAuraAppliedEvent.Duration.Should().Be(1.2 * 6.0);
    }
}
