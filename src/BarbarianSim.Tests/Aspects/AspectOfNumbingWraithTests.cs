using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfNumbingWraithTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfNumbingWraith _aspect;

    public AspectOfNumbingWraithTests()
    {
        _aspect = new AspectOfNumbingWraith(_mockSimLogger.Object) { Fortify = 54 };
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Creates_FortifyGeneratedEvent()
    {
        var furyGeneratedEvent = new FuryGeneratedEvent(123, null, 20) { OverflowFury = 2 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Amount.Should().Be(108);
    }

    [Fact]
    public void Does_Nothing_When_No_Overflow_Fury()
    {
        var furyGeneratedEvent = new FuryGeneratedEvent(123, null, 20);

        _aspect.ProcessEvent(furyGeneratedEvent, _state);

        _state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        var furyGeneratedEvent = new FuryGeneratedEvent(123, null, 20) { OverflowFury = 10 };

        _aspect.ProcessEvent(furyGeneratedEvent, _state);
        _state.Player.Fortify.Should().Be(0);
    }
}
