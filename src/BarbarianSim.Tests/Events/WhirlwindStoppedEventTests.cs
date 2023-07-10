using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WhirlwindStoppedEventTests
{
    [Fact]
    public void Creates_WhirlwindingAuraExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());

        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(123);
        whirlwindStoppedEvent.ProcessEvent(state);

        whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent && ((AuraExpiredEvent)e).Aura == Aura.Whirlwinding);
        whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent.Timestamp.Should().Be(123);
        whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent.Aura.Should().Be(Aura.Whirlwinding);
    }

    [Fact]
    public void Creates_ViolentWhirlwindAuraExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());

        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(123);
        whirlwindStoppedEvent.ProcessEvent(state);

        whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent.Should().NotBeNull();
        state.Events.Should().Contain(whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent);
        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent && ((AuraExpiredEvent)e).Aura == Aura.ViolentWhirlwind);
        whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent.Timestamp.Should().Be(123);
        whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent.Aura.Should().Be(Aura.ViolentWhirlwind);
    }

    [Fact]
    public void Removes_ViolentWhirlwindAuraAppliedEvents()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Events.Add(new AuraAppliedEvent(123, 5, Aura.ViolentWhirlwind));

        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(100);
        whirlwindStoppedEvent.ProcessEvent(state);

        state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ViolentWhirlwind);
    }
}
