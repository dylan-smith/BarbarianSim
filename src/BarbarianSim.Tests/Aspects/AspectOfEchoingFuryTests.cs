using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfEchoingFuryTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfEchoingFury _aspect = new();

    [Fact]
    public void Creates_ProcEvent_On_ChallengingShout()
    {
        _aspect.Fury = 3;
        var shoutEvent = new ChallengingShoutEvent(123)
        {
            Duration = 12,
        };

        _aspect.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(3);
    }

    [Fact]
    public void Creates_ProcEvent_On_WarCry()
    {
        _aspect.Fury = 3;
        var shoutEvent = new WarCryEvent(123)
        {
            Duration = 12,
        };

        _aspect.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(3);
    }

    [Fact]
    public void Creates_ProcEvent_On_RallyingCry()
    {
        _aspect.Fury = 3;
        var shoutEvent = new RallyingCryEvent(123)
        {
            Duration = 12,
        };

        _aspect.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(3);
    }
}
