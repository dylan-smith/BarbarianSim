using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class TwoHandedMaceExpertiseProcEventHandlerTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly TwoHandedMaceExpertiseProcEventHandler _handler;

    public TwoHandedMaceExpertiseProcEventHandlerTests() => _handler = new(_mockSimLogger.Object);

    [Fact]
    public void Creates_FuryGeneratedEvent()
    {
        var procEvent = new TwoHandedMaceExpertiseProcEvent(123.0);

        _handler.ProcessEvent(procEvent, _state);

        procEvent.FuryGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(procEvent.FuryGeneratedEvent);
        procEvent.FuryGeneratedEvent.Timestamp.Should().Be(123);
        procEvent.FuryGeneratedEvent.BaseFury.Should().Be(2);
    }

    [Fact]
    public void Double_Fury_When_Berserking()
    {
        _state.Player.Auras.Add(Aura.Berserking);
        var procEvent = new TwoHandedMaceExpertiseProcEvent(123.0);

        _handler.ProcessEvent(procEvent, _state);

        procEvent.FuryGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(procEvent.FuryGeneratedEvent);
        procEvent.FuryGeneratedEvent.Timestamp.Should().Be(123);
        procEvent.FuryGeneratedEvent.BaseFury.Should().Be(4);
    }
}
