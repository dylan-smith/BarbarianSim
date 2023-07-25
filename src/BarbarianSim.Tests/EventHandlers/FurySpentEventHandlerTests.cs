using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class FurySpentEventHandlerTests
{
    private readonly Mock<FuryCostReductionCalculator> _mockFuryCostReductionCalculator = TestHelpers.CreateMock<FuryCostReductionCalculator>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly FurySpentEventHandler _handler;

    public FurySpentEventHandlerTests()
    {
        _mockFuryCostReductionCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<SkillType>()))
                                        .Returns(1.0);

        _handler = new FurySpentEventHandler(_mockFuryCostReductionCalculator.Object);
    }

    [Fact]
    public void Removes_Fury_From_Player()
    {
        _state.Player.Fury = 29.0;
        var furySpentEvent = new FurySpentEvent(123.0, 12.0, SkillType.Basic);

        _handler.ProcessEvent(furySpentEvent, _state);

        _state.Player.Fury.Should().Be(17.0);
    }

    [Fact]
    public void Applies_FuryCostReduction_Multiplier()
    {
        _state.Player.Fury = 29.0;
        _mockFuryCostReductionCalculator.Setup(m => m.Calculate(_state, SkillType.Core))
                                        .Returns(0.8);

        var furySpentEvent = new FurySpentEvent(123.0, 12.0, SkillType.Core);

        _handler.ProcessEvent(furySpentEvent, _state);

        _state.Player.Fury.Should().Be(19.4);
    }

    [Fact]
    public void Player_Fury_Cant_Go_Below_0()
    {
        _state.Player.Fury = 10.0;
        var furySpentEvent = new FurySpentEvent(123.0, 12.0, SkillType.Basic);

        _handler.ProcessEvent(furySpentEvent, _state);

        _state.Player.Fury.Should().Be(0.0);
    }
}
