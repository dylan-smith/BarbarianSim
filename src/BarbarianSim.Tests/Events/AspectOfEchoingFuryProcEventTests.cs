using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public sealed class AspectOfEchoingFuryProcEventTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Creates_FuryGeneratedEvents_For_Each_Second()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspectProcEvent = new AspectOfEchoingFuryProcEvent(123.0, 6.0, 4);

        aspectProcEvent.ProcessEvent(state);

        aspectProcEvent.FuryGeneratedEvents.Should().HaveCount(6);
        state.Events.OfType<FuryGeneratedEvent>().Should().HaveCount(6);
        aspectProcEvent.FuryGeneratedEvents.First().Timestamp.Should().Be(124);
        aspectProcEvent.FuryGeneratedEvents.Last().Timestamp.Should().Be(129);
        state.Events.OfType<FuryGeneratedEvent>().All(e => e.BaseFury == 4).Should().BeTrue();
    }

    [Fact]
    public void Rounds_Down_Duration()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspectProcEvent = new AspectOfEchoingFuryProcEvent(123.0, 3.96, 4);

        aspectProcEvent.ProcessEvent(state);

        aspectProcEvent.FuryGeneratedEvents.Should().HaveCount(3);
    }
}
