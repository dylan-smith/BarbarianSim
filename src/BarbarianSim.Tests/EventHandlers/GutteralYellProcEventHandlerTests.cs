using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class GutteralYellProcEventHandlerTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly GutteralYellProcEventHandler _handler;

    public GutteralYellProcEventHandlerTests() => _handler = new(_mockSimLogger.Object);

    [Fact]
    public void Creates_AuraAppliedEvent()
    {
        var gutteralYellProcEvent = new GutteralYellProcEvent(123);

        _handler.ProcessEvent(gutteralYellProcEvent, _state);

        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Should().NotBeNull();
        _state.Events.Should().Contain(gutteralYellProcEvent.GutteralYellAuraAppliedEvent);
        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent);
        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Timestamp.Should().Be(123);
        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Duration.Should().Be(5);
        gutteralYellProcEvent.GutteralYellAuraAppliedEvent.Aura.Should().Be(Aura.GutteralYell);
    }
}
