using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class VulnerableAppliedEventTests
{
    [Fact]
    public void Adds_Vulnerable_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        var vulnerableAppliedEvent = new VulnerableAppliedEvent(123, state.Enemies.First(), 2.0);

        vulnerableAppliedEvent.ProcessEvent(state);

        state.Enemies.First().Auras.Should().Contain(Aura.Vulnerable);
    }

    [Fact]
    public void Creates_VulnerableExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var vulnerableAppliedEvent = new VulnerableAppliedEvent(123, state.Enemies.First(), 2.0);

        vulnerableAppliedEvent.ProcessEvent(state);

        vulnerableAppliedEvent.VulnerableExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(vulnerableAppliedEvent.VulnerableExpiredEvent);
        state.Events.Should().ContainSingle(e => e is VulnerableExpiredEvent);
        vulnerableAppliedEvent.VulnerableExpiredEvent.Timestamp.Should().Be(125);
        vulnerableAppliedEvent.VulnerableExpiredEvent.Target.Should().Be(state.Enemies.First());
    }
}
