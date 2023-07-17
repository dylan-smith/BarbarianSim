using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class FuryGeneratedEventHandlerTests
{
    private readonly Mock<ResourceGenerationCalculator> _mockResourceGenerationCalculator = TestHelpers.CreateMock<ResourceGenerationCalculator>();
    private readonly Mock<MaxFuryCalculator> _mockMaxFuryCalculator = TestHelpers.CreateMock<MaxFuryCalculator>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly FuryGeneratedEventHandler _handler;

    public FuryGeneratedEventHandlerTests()
    {
        _mockResourceGenerationCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                                         .Returns(1.0);

        _mockMaxFuryCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(100);

        _handler = new FuryGeneratedEventHandler(_mockResourceGenerationCalculator.Object, _mockMaxFuryCalculator.Object);
    }

    [Fact]
    public void Adds_Fury_To_Player()
    {
        var furyGeneratedEvent = new FuryGeneratedEvent(123.0, 12.0);

        _handler.ProcessEvent(furyGeneratedEvent, _state);

        _state.Player.Fury.Should().Be(12.0);
    }

    [Fact]
    public void Applies_Resource_Generation_Multiplier()
    {
        _mockResourceGenerationCalculator.Setup(m => m.Calculate(_state))
                                         .Returns(1.4);

        var furyGeneratedEvent = new FuryGeneratedEvent(123.0, 12.0);

        _handler.ProcessEvent(furyGeneratedEvent, _state);

        _state.Player.Fury.Should().BeApproximately(16.8, 0.00001);
    }

    [Fact]
    public void Caps_Fury_At_Max()
    {
        _mockMaxFuryCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(130);
        _state.Player.Fury = 125;
        var furyGeneratedEvent = new FuryGeneratedEvent(123.0, 12.0);

        _handler.ProcessEvent(furyGeneratedEvent, _state);

        _state.Player.Fury.Should().Be(130.0);
    }
}
