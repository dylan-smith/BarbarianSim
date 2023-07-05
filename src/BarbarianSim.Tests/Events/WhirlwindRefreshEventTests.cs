using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class WhirlwindRefreshEventTests
{
    [Fact]
    public void Creates_WhirlwindStartedEvent_If_CanRefresh_Is_True()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 100;
        state.Player.Auras.Add(Aura.Whirlwinding);

        var whirlwindRefreshEvent = new WhirlwindRefreshEvent(123.0);
        whirlwindRefreshEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindRefreshEvent.WhirlwindStartedEvent);
        state.Events.Should().ContainSingle(e => e is WhirlwindStartedEvent);
        state.Events.First().Timestamp.Should().Be(123.0);
    }

    [Fact]
    public void Creates_WhirlwindStoppedEvent_If_CanRefresh_Is_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 0;

        var whirlwindRefreshEvent = new WhirlwindRefreshEvent(123.0);
        whirlwindRefreshEvent.ProcessEvent(state);

        state.Events.Should().Contain(whirlwindRefreshEvent.WhirlwindStoppedEvent);
        state.Events.Should().ContainSingle(e => e is WhirlwindStoppedEvent);
        state.Events.First().Timestamp.Should().Be(123.0);
    }
}
