using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class InvigoratingFuryTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly InvigoratingFury _skill;

    public InvigoratingFuryTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200);
        _skill = new InvigoratingFury(_mockMaxLifeCalculator.Object, _mockSimLogger.Object);
    }

    [Fact]
    public void Creates_HealingEvent()
    {
        _state.Config.Skills.Add(Skill.InvigoratingFury, 1);
        var furySpentEvent = new FurySpentEvent(123, null, 100, SkillType.Basic)
        {
            FurySpent = 100
        };
        _state.ProcessedEvents.Add(furySpentEvent);

        _skill.ProcessEvent(furySpentEvent, _state);

        _state.Events.Should().ContainSingle(e => e is HealingEvent);
        _state.Events.OfType<HealingEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<HealingEvent>().First().BaseAmountHealed.Should().Be(1200 * 0.03);
    }

    [Fact]
    public void Does_Not_Proc_When_Fury_Not_Crossing_100_Threshold()
    {
        _state.Config.Skills.Add(Skill.InvigoratingFury, 1);
        var previousFuryEvent = new FurySpentEvent(110, null, 25, SkillType.Basic)
        {
            FurySpent = 25
        };
        _state.ProcessedEvents.Add(previousFuryEvent);
        var furySpentEvent = new FurySpentEvent(123, null, 70, SkillType.Basic)
        {
            FurySpent = 70
        };
        _state.ProcessedEvents.Add(furySpentEvent);

        _skill.ProcessEvent(furySpentEvent, _state);

        _state.Events.Should().NotContain(e => e is HealingEvent);
    }

    [Fact]
    public void Procs_When_Fury_Crosses_100_Threshold()
    {
        _state.Config.Skills.Add(Skill.InvigoratingFury, 1);
        var previousFuryEvent = new FurySpentEvent(110, null, 25, SkillType.Basic)
        {
            FurySpent = 35
        };
        _state.ProcessedEvents.Add(previousFuryEvent);
        var furySpentEvent = new FurySpentEvent(123, null, 70, SkillType.Basic)
        {
            FurySpent = 70
        };
        _state.ProcessedEvents.Add(furySpentEvent);

        _skill.ProcessEvent(furySpentEvent, _state);

        _state.Events.Should().ContainSingle(e => e is HealingEvent);
    }

    [Fact]
    public void Does_Not_Proc_If_Skill_Points_Are_Zero()
    {
        var furySpentEvent = new FurySpentEvent(123, null, 100, SkillType.Basic);

        _skill.ProcessEvent(furySpentEvent, _state);

        _state.Events.Should().NotContain(e => e is HealingEvent);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0.03)]
    [InlineData(2, 0.06)]
    [InlineData(3, 0.09)]
    [InlineData(4, 0.09)]
    public void Skill_Points_Determines_Percent_Proc(int skillPoints, double healingPercentage)
    {
        _state.Config.Skills.Add(Skill.InvigoratingFury, skillPoints);

        _skill.GetHealingPercentage(_state).Should().Be(healingPercentage);
    }
}
