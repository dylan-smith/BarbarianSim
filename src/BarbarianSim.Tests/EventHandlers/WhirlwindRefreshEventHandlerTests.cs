using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class WhirlwindRefreshEventHandlerTests
{
    private readonly Mock<Whirlwind> _mockWhirlwind = TestHelpers.CreateMock<Whirlwind>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WhirlwindRefreshEventHandler _handler;

    public WhirlwindRefreshEventHandlerTests()
    {
        _mockWhirlwind.Setup(m => m.CanRefresh(It.IsAny<SimulationState>()))
                      .Returns(true);

        _handler = new WhirlwindRefreshEventHandler(_mockWhirlwind.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Creates_WhirlwindSpinEvent_If_CanRefresh_Is_True()
    {
        var whirlwindRefreshEvent = new WhirlwindRefreshEvent(123.0);
        _handler.ProcessEvent(whirlwindRefreshEvent, _state);

        _state.Events.Should().Contain(whirlwindRefreshEvent.WhirlwindSpinEvent);
        _state.Events.Should().ContainSingle(e => e is WhirlwindSpinEvent);
        _state.Events.First().Timestamp.Should().Be(123.0);
    }

    [Fact]
    public void Creates_WhirlwindStoppedEvent_If_CanRefresh_Is_False()
    {
        _mockWhirlwind.Setup(m => m.CanRefresh(It.IsAny<SimulationState>()))
                      .Returns(false);

        var whirlwindRefreshEvent = new WhirlwindRefreshEvent(123.0);
        _handler.ProcessEvent(whirlwindRefreshEvent, _state);

        _state.Events.Should().Contain(whirlwindRefreshEvent.WhirlwindStoppedEvent);
        _state.Events.Should().ContainSingle(e => e is WhirlwindStoppedEvent);
        _state.Events.First().Timestamp.Should().Be(123.0);
    }
}
