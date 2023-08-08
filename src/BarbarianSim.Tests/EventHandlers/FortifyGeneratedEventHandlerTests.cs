using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class FortifyGeneratedEventHandlerTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly FortifyGeneratedEventHandler _handler;

    public FortifyGeneratedEventHandlerTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(1000);

        _handler = new FortifyGeneratedEventHandler(_mockMaxLifeCalculator.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Adds_Fortify_To_Player()
    {
        var fortifyGeneratedEvent = new FortifyGeneratedEvent(123.0, null, 12.0);

        _handler.ProcessEvent(fortifyGeneratedEvent, _state);

        _state.Player.Fortify.Should().Be(12.0);
    }

    [Fact]
    public void Caps_Fortify_At_Max_Life()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(4000);
        _state.Player.Fortify = 3970;
        var fortifyGeneratedEvent = new FortifyGeneratedEvent(123.0, null, 80.0);

        _handler.ProcessEvent(fortifyGeneratedEvent, _state);

        _state.Player.Fortify.Should().Be(4000);
    }


}
