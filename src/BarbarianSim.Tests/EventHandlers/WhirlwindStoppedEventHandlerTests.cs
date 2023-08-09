using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class WhirlwindStoppedEventHandlerTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new new(new SimulationConfig());
    private readonly WhirlwindStoppedEventHandler _handler;
    
    public WhirlwindStoppedEventHandlerTests() => _handler = new WhirlwindStoppedEventHandler(_mockSimLogger.Object);
    
    [Fact]
    public void Creates_WhirlwindingAuraExpiredEvent()
    {
        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(123);
        _handler.ProcessEvent(whirlwindStoppedEvent, _state);

        whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent.Should().NotBeNull();
        _state.Events.Should().Contain(whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent);
        _state.Events.Should().ContainSingle(e => e is AuraExpiredEvent && ((AuraExpiredEvent)e).Aura == Aura.Whirlwinding);
        whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent.Timestamp.Should().Be(123);
        whirlwindStoppedEvent.WhirlwindingAuraExpiredEvent.Aura.Should().Be(Aura.Whirlwinding);
    }

    [Fact]
    public void Creates_ViolentWhirlwindAuraExpiredEvent()
    {
        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(123);
        _handler.ProcessEvent(whirlwindStoppedEvent, _state);

        whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent.Should().NotBeNull();
        _state.Events.Should().Contain(whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent);
        _state.Events.Should().ContainSingle(e => e is AuraExpiredEvent && ((AuraExpiredEvent)e).Aura == Aura.ViolentWhirlwind);
        whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent.Timestamp.Should().Be(123);
        whirlwindStoppedEvent.ViolentWhirlwindAuraExpiredEvent.Aura.Should().Be(Aura.ViolentWhirlwind);
    }

    [Fact]
    public void Removes_ViolentWhirlwindAuraAppliedEvents()
    {
        _state.Events.Add(new AuraAppliedEvent(123, null, 5, Aura.ViolentWhirlwind));

        var whirlwindStoppedEvent = new WhirlwindStoppedEvent(100);
        _handler.ProcessEvent(whirlwindStoppedEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.ViolentWhirlwind);
    }
}
