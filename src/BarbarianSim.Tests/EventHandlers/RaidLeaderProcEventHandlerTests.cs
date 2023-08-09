using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class RaidLeaderProcEventHandlerTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<RaidLeader> _mockRaidLeader = TestHelpers.CreateMock<RaidLeader>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly RaidLeaderProcEventHandler _handler;

    public RaidLeaderProcEventHandlerTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>()))
                              .Returns(1200);

        _mockRaidLeader.Setup(m => m.GetHealPercentage(It.IsAny<SimulationState>()))
                     .Returns(0.02);

        _handler = new RaidLeaderProcEventHandler(_mockMaxLifeCalculator.Object, _mockRaidLeader.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Creates_Healing_Events_For_Each_Second()
    {
        var raidLeaderProcEvent = new RaidLeaderProcEvent(123.0, 6.0);

        _handler.ProcessEvent(raidLeaderProcEvent, _state);

        raidLeaderProcEvent.HealingEvents.Should().HaveCount(6);
        _state.Events.OfType<HealingEvent>().Should().HaveCount(6);
        raidLeaderProcEvent.HealingEvents.First().Timestamp.Should().Be(124);
        raidLeaderProcEvent.HealingEvents.Last().Timestamp.Should().Be(129);
    }

    [Fact]
    public void Heal_Percentage_Based_On_RaidLeader_Skill()
    {
        _state.Config.Skills.Add(Skill.RaidLeader, 3);
        var raidLeaderProcEvent = new RaidLeaderProcEvent(123.0, 6.0);

        _handler.ProcessEvent(raidLeaderProcEvent, _state);

        raidLeaderProcEvent.HealingEvents.All(e => Math.Abs(e.BaseAmountHealed - 24.0) < 0.00001).Should().BeTrue();
    }

    [Fact]
    public void Rounds_Down_Duration()
    {
        var raidLeaderProcEvent = new RaidLeaderProcEvent(123.0, 3.96);

        _handler.ProcessEvent(raidLeaderProcEvent, _state);

        raidLeaderProcEvent.HealingEvents.Should().HaveCount(3);
    }
}
