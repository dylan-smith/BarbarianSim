using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfEchoingFuryTests
{
    [Fact]
    public void Creates_ProcEvent_On_ChallengingShout()
    {
        var state = new SimulationState(new SimulationConfig());

        var aspect = new AspectOfEchoingFury(3);
        var shoutEvent = new ChallengingShoutEvent(123)
        {
            Duration = 12,
        };

        aspect.ProcessEvent(shoutEvent, state);

        state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(3);
    }

    [Fact]
    public void Creates_ProcEvent_On_WarCry()
    {
        var state = new SimulationState(new SimulationConfig());

        var aspect = new AspectOfEchoingFury(3);
        var shoutEvent = new WarCryEvent(123)
        {
            Duration = 12,
        };

        aspect.ProcessEvent(shoutEvent, state);

        state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(3);
    }

    [Fact]
    public void Creates_ProcEvent_On_RallyingCry()
    {
        var state = new SimulationState(new SimulationConfig());

        var aspect = new AspectOfEchoingFury(3);
        var shoutEvent = new RallyingCryEvent(123)
        {
            Duration = 12,
        };

        aspect.ProcessEvent(shoutEvent, state);

        state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(3);
    }
}
