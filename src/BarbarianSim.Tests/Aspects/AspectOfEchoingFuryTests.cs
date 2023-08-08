using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfEchoingFuryTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfEchoingFury _aspect;

    public AspectOfEchoingFuryTests()
    {
        _aspect = new(_mockSimLogger.Object);
        _state.Config.Gear.Helm.Aspect = _aspect;
        _aspect.Fury = 4;
    }

    [Fact]
    public void Creates_ProcEvent_On_ChallengingShout()
    {
        var shoutEvent = new ChallengingShoutEvent(123)
        {
            Duration = 12,
        };

        _aspect.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(4);
    }

    [Fact]
    public void Creates_ProcEvent_On_WarCry()
    {
        var shoutEvent = new WarCryEvent(123)
        {
            Duration = 12,
        };

        _aspect.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(4);
    }

    [Fact]
    public void Creates_ProcEvent_On_RallyingCry()
    {
        var shoutEvent = new RallyingCryEvent(123)
        {
            Duration = 12,
        };

        _aspect.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfEchoingFuryProcEvent);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Duration.Should().Be(12);
        _state.Events.OfType<AspectOfEchoingFuryProcEvent>().First().Fury.Should().Be(4);
    }

    [Fact]
    public void Does_Not_Proc_If_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        var rallyingCryEvent = new RallyingCryEvent(123) { Duration = 12 };
        var warCryEvent = new WarCryEvent(123) { Duration = 12 };
        var challengingShoutEvent = new ChallengingShoutEvent(123) { Duration = 12 };

        _aspect.ProcessEvent(rallyingCryEvent, _state);
        _aspect.ProcessEvent(warCryEvent, _state);
        _aspect.ProcessEvent(challengingShoutEvent, _state);

        _state.Events.Should().NotContain(e => e is AspectOfEchoingFuryProcEvent);
    }
}
