using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.Paragon;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class WarbringerProcEventHandlerTests
{
    private readonly Mock<Warbringer> _mockWarbringer = TestHelpers.CreateMock<Warbringer>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WarbringerProcEventHandler _handler;

    public WarbringerProcEventHandlerTests()
    {
        _mockWarbringer.Setup(m => m.GetFortifyGenerated(It.IsAny<SimulationState>()))
                       .Returns(120);

        _handler = new WarbringerProcEventHandler(_mockWarbringer.Object);
    }

    [Fact]
    public void Creates_FortifyGeneratedEvent()
    {
        var warbringerProcEvent = new WarbringerProcEvent(123.0);

        _handler.ProcessEvent(warbringerProcEvent, _state);

        warbringerProcEvent.FortifyGeneratedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(warbringerProcEvent.FortifyGeneratedEvent);
        warbringerProcEvent.FortifyGeneratedEvent.Timestamp.Should().Be(123);
        warbringerProcEvent.FortifyGeneratedEvent.Amount.Should().Be(120);
    }
}
