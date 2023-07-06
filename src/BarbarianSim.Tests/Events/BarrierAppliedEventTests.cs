﻿using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class BarrierAppliedEventTests
{
    [Fact]
    public void Adds_Barrier_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BarrierAppliedEvent(123.0, 1000, 10.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.Barrier);
    }

    [Fact]
    public void Adds_Barrier_To_Player()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BarrierAppliedEvent(123.0, 1000, 10.0);

        e.ProcessEvent(state);

        state.Player.Barriers.Should().Contain(e.Barrier);
        state.Player.Barriers.Should().HaveCount(1);
        e.Barrier.Amount.Should().Be(1000);
    }

    [Fact]
    public void Creates_BarrierExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BarrierAppliedEvent(123.0, 1000, 10.0);

        e.ProcessEvent(state);

        state.Events.Should().Contain(e.BarrierExpiredEvent);
        state.Events.Should().ContainSingle(e => e is BarrierExpiredEvent);
        e.BarrierExpiredEvent.Timestamp.Should().Be(133.0);
        e.BarrierExpiredEvent.Barrier.Should().Be(e.Barrier);
    }
}
