using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class GohrsDevastatingGripsProcEventHandlerTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly GohrsDevastatingGripsProcEventHandler _handler;

    public GohrsDevastatingGripsProcEventHandlerTests() => _handler = new(_mockSimLogger.Object);

    [Fact]
    public void Creates_DamageEvents()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        var gohrsDevastatingGripsProcEvent = new GohrsDevastatingGripsProcEvent(123.0, 1200.0);

        _handler.ProcessEvent(gohrsDevastatingGripsProcEvent, state);

        state.Events.Should().HaveCount(2);
        state.Events.Should().Contain(gohrsDevastatingGripsProcEvent.DamageEvents[0]);
        state.Events.Should().Contain(gohrsDevastatingGripsProcEvent.DamageEvents[1]);
        gohrsDevastatingGripsProcEvent.DamageEvents[0].Damage.Should().Be(1200.0);
        gohrsDevastatingGripsProcEvent.DamageEvents[1].Damage.Should().Be(1200.0);
        gohrsDevastatingGripsProcEvent.DamageEvents[0].DamageSource.Should().Be(DamageSource.GohrsDevastatingGrips);
        gohrsDevastatingGripsProcEvent.DamageEvents[1].DamageSource.Should().Be(DamageSource.GohrsDevastatingGrips);
        gohrsDevastatingGripsProcEvent.DamageEvents[0].SkillType.Should().Be(SkillType.None);
        gohrsDevastatingGripsProcEvent.DamageEvents[1].SkillType.Should().Be(SkillType.None);
    }
}
