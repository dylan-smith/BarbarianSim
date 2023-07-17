using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class AuraAppliedEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AuraAppliedEventHandler _handler = new();

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
}
