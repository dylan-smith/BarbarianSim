using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class HealingEventHandlerTests
{
    private readonly Mock<HealingReceivedCalculator> _mockHealingReceivedCalculator = TestHelpers.CreateMock<HealingReceivedCalculator>();
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly HealingEventHandler _handler;

    public HealingEventHandlerTests()
    {
        _mockHealingReceivedCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                                      .Returns(1.0);

        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(1200);

        _handler = new HealingEventHandler(_mockHealingReceivedCalculator.Object, _mockMaxLifeCalculator.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Increases_Player_Life()
    {
        _state.Player.Life = 500.0;
        var healingEvent = new HealingEvent(123.0, null, 270.0);

        _handler.ProcessEvent(healingEvent, _state);

        _state.Player.Life.Should().Be(770.0);
        healingEvent.BaseAmountHealed.Should().Be(270.0);
        healingEvent.AmountHealed.Should().Be(270.0);
        healingEvent.OverHeal.Should().Be(0.0);
    }

    [Fact]
    public void Applies_HealingReceived_Multiplier()
    {
        _state.Player.Life = 500.0;
        _mockHealingReceivedCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                                      .Returns(1.5);
        var healingEvent = new HealingEvent(123.0, null, 270.0);

        _handler.ProcessEvent(healingEvent, _state);

        _state.Player.Life.Should().Be(905.0);
    }

    [Fact]
    public void Calculates_Overheal()
    {
        _state.Player.Life = 700.0;
        var healingEvent = new HealingEvent(123.0, null, 810.0);

        _handler.ProcessEvent(healingEvent, _state);

        _state.Player.Life.Should().Be(1200.0);
        healingEvent.BaseAmountHealed.Should().Be(810.0);
        healingEvent.AmountHealed.Should().Be(500.0);
        healingEvent.OverHeal.Should().Be(310.0);
    }
}
