using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class AuraAppliedEventTests
{
    [Fact]
    public void Applies_Aura_To_Player()
    {
        var testAura = Aura.WarCry;

        var state = new SimulationState(new SimulationConfig());
        var auraAppliedEvent = new AuraAppliedEvent(123.0, 5, testAura);

        auraAppliedEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(testAura);
    }

    [Fact]
    public void Creates_AuraExpiredEvent()
    {
        var testAura = Aura.WarCry;

        var state = new SimulationState(new SimulationConfig());
        var auraAppliedEvent = new AuraAppliedEvent(123.0, 5, testAura);

        auraAppliedEvent.ProcessEvent(state);

        auraAppliedEvent.AuraExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(auraAppliedEvent.AuraExpiredEvent);
        state.Events.Should().Contain(e => e is AuraExpiredEvent);
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

        auraAppliedEvent.ProcessEvent(state);

        state.Enemies.Last().Auras.Should().Contain(testAura);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent && ((AuraExpiredEvent)e).Target == state.Enemies.Last());
    }

    [Fact]
    public void Zero_Duration_Has_No_AuraExpiredEvent()
    {
        var testAura = Aura.WarCry;

        var state = new SimulationState(new SimulationConfig());
        var auraAppliedEvent = new AuraAppliedEvent(123.0, 0, testAura);

        auraAppliedEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(testAura);
        state.Events.Should().NotContain(e => e is AuraExpiredEvent);
    }
}
