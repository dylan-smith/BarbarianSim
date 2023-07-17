using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class VulnerableAppliedEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly VulnerableAppliedEventHandler _handler = new();

    [Fact]
    public void Adds_Vulnerable_Aura()
    {
        var vulnerableAppliedEvent = new VulnerableAppliedEvent(123, _state.Enemies.First(), 2.0);

        _handler.ProcessEvent(vulnerableAppliedEvent, _state);

        _state.Enemies.First().Auras.Should().Contain(Aura.Vulnerable);
    }

    [Fact]
    public void Creates_VulnerableExpiredEvent()
    {
        var vulnerableAppliedEvent = new VulnerableAppliedEvent(123, _state.Enemies.First(), 2.0);

        _handler.ProcessEvent(vulnerableAppliedEvent, _state);

        vulnerableAppliedEvent.VulnerableExpiredEvent.Should().NotBeNull();
        _state.Events.Should().Contain(vulnerableAppliedEvent.VulnerableExpiredEvent);
        _state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        vulnerableAppliedEvent.VulnerableExpiredEvent.Timestamp.Should().Be(125);
        vulnerableAppliedEvent.VulnerableExpiredEvent.Target.Should().Be(_state.Enemies.First());
        vulnerableAppliedEvent.VulnerableExpiredEvent.Aura.Should().Be(Aura.Vulnerable);
    }
}
