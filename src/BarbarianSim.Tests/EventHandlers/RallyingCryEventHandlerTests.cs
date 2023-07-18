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
    public void BoomingVoice_Increases_Duration()
    {
        _mockBoomingVoice.Setup(m => m.GetDurationIncrease(_state))
                         .Returns(1.2);

        var rallyingCryEvent = new RallyingCryEvent(123);

        _handler.ProcessEvent(rallyingCryEvent, _state);

        rallyingCryEvent.RallyingCryAuraAppliedEvent.Duration.Should().Be(1.2 * 6.0);
    }
}
