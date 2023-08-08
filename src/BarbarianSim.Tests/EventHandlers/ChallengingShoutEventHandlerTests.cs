using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class ChallengingShoutEventHandlerTests
{
    private readonly Mock<BoomingVoice> _mockBoomingVoice = TestHelpers.CreateMock<BoomingVoice>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly ChallengingShoutEventHandler _handler;

    public ChallengingShoutEventHandlerTests()
    {
        _mockBoomingVoice.Setup(m => m.GetDurationIncrease(It.IsAny<SimulationState>()))
                         .Returns(1.0);

        _handler = new ChallengingShoutEventHandler(_mockBoomingVoice.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Creates_ChallengingShoutCooldownAuraAppliedEvent()
    {
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        _handler.ProcessEvent(challengingShoutEvent, _state);

        challengingShoutEvent.ChallengingShoutCooldownAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(challengingShoutEvent.ChallengingShoutCooldownAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ChallengingShoutCooldown);
        challengingShoutEvent.ChallengingShoutCooldownAuraAppliedEvent.Timestamp.Should().Be(123);
        challengingShoutEvent.ChallengingShoutCooldownAuraAppliedEvent.Duration.Should().Be(25);
        challengingShoutEvent.ChallengingShoutCooldownAuraAppliedEvent.Aura.Should().Be(Aura.ChallengingShoutCooldown);
    }

    [Fact]
    public void Creates_ChallengingShoutAuraAppliedEvent()
    {
        var challengingShoutEvent = new ChallengingShoutEvent(123);

        _handler.ProcessEvent(challengingShoutEvent, _state);

        challengingShoutEvent.ChallengingShoutAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(challengingShoutEvent.ChallengingShoutAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ChallengingShout);
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

        _handler.ProcessEvent(challengingShoutEvent, state);

        challengingShoutEvent.TauntAuraAppliedEvent.Should().HaveCount(3);
        state.Events.Count(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Should().Be(3);
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().All(e => e.Timestamp == 123).Should().BeTrue();
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().All(e => e.Duration == 6).Should().BeTrue();
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().First().Target.Should().Be(state.Enemies.First());
        state.Events.Where(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.Taunt).Cast<AuraAppliedEvent>().Last().Target.Should().Be(state.Enemies.Last());
    }

    [Fact]
    public void BoomingVoice_Increases_Duration()
    {
        _mockBoomingVoice.Setup(m => m.GetDurationIncrease(_state)).Returns(1.24);

        var challengingShoutEvent = new ChallengingShoutEvent(123);

        _handler.ProcessEvent(challengingShoutEvent, _state);

        challengingShoutEvent.ChallengingShoutAuraAppliedEvent.Duration.Should().Be(6.0 * 1.24);
    }
}
