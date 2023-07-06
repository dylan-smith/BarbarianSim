using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class RallyingCryEventTests
{
    [Fact]
    public void Adds_RallyingCry_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.RallyingCry);
    }

    [Fact]
    public void Adds_RallyingCryCooldown_Aura_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.RallyingCryCooldown);
    }

    [Fact]
    public void Creates_RallyingCryCooldownCompletedEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.RallyingCryCooldownCompletedEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.RallyingCryCooldownCompletedEvent);
        state.Events.Should().ContainSingle(e => e is RallyingCryCooldownCompletedEvent);
        rallyingCryEvent.RallyingCryCooldownCompletedEvent.Timestamp.Should().Be(148);
    }

    [Fact]
    public void Creates_RallyingCryExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var rallyingCryEvent = new RallyingCryEvent(123);

        rallyingCryEvent.ProcessEvent(state);

        rallyingCryEvent.RallyingCryExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(rallyingCryEvent.RallyingCryExpiredEvent);
        state.Events.Should().ContainSingle(e => e is RallyingCryExpiredEvent);
        rallyingCryEvent.RallyingCryExpiredEvent.Timestamp.Should().Be(129);

    }
}
