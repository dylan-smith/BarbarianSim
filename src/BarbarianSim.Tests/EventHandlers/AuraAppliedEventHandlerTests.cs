using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class AuraAppliedEventHandlerTests
{
    private readonly Mock<CrowdControlDurationCalculator> _mockCrowdControlDurationCalculator = TestHelpers.CreateMock<CrowdControlDurationCalculator>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AuraAppliedEventHandler _handler;

    public AuraAppliedEventHandlerTests()
    {
        _mockCrowdControlDurationCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<double>())).Returns(1.0);
        _handler = new AuraAppliedEventHandler(_mockCrowdControlDurationCalculator.Object);
    }

    [Fact]
    public void Applies_Aura_To_Player()
    {
        var testAura = Aura.WarCry;

        var auraAppliedEvent = new AuraAppliedEvent(123.0, 5, testAura);

        _handler.ProcessEvent(auraAppliedEvent, _state);

        _state.Player.Auras.Should().Contain(testAura);
    }

    [Fact]
    public void Creates_AuraExpiredEvent()
    {
        var testAura = Aura.WarCry;

        var auraAppliedEvent = new AuraAppliedEvent(123.0, 5, testAura);

        _handler.ProcessEvent(auraAppliedEvent, _state);

        auraAppliedEvent.AuraExpiredEvent.Should().NotBeNull();
        _state.Events.Should().Contain(auraAppliedEvent.AuraExpiredEvent);
        _state.Events.Should().Contain(e => e is AuraExpiredEvent);
        auraAppliedEvent.AuraExpiredEvent.Aura.Should().Be(testAura);
        auraAppliedEvent.AuraExpiredEvent.Timestamp.Should().Be(128);
    }

    [Fact]
    public void Applies_Aura_To_Enemy()
    {
        var testAura = Aura.WarCry;

        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 3;
        var state = new SimulationState(config);
        var auraAppliedEvent = new AuraAppliedEvent(123.0, 5, testAura, state.Enemies.Last());

        _handler.ProcessEvent(auraAppliedEvent, state);

        state.Enemies.Last().Auras.Should().Contain(testAura);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent && ((AuraExpiredEvent)e).Target == state.Enemies.Last());
    }

    [Fact]
    public void Zero_Duration_Has_No_AuraExpiredEvent()
    {
        var testAura = Aura.WarCry;

        var auraAppliedEvent = new AuraAppliedEvent(123.0, 0, testAura);

        _handler.ProcessEvent(auraAppliedEvent, _state);

        _state.Player.Auras.Should().Contain(testAura);
        _state.Events.Should().NotContain(e => e is AuraExpiredEvent);
    }

    [Fact]
    public void Applies_CrowdControlDuration_Bonuses()
    {
        _mockCrowdControlDurationCalculator.Setup(m => m.Calculate(_state, 5)).Returns(7);

        var auraAppliedEvent = new AuraAppliedEvent(123.0, 5, Aura.Stun);

        _handler.ProcessEvent(auraAppliedEvent, _state);

        auraAppliedEvent.AuraExpiredEvent.Timestamp.Should().Be(123 + 7);
    }
}
