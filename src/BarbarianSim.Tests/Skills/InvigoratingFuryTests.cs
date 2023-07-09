using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public sealed class InvigoratingFuryTests : IDisposable
{
    public void Dispose()
    {
        BaseStatCalculator.ClearMocks();
    }

    [Fact]
    public void Creates_HealingEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.InvigoratingFury, 1);
        var furySpentEvent = new FurySpentEvent(123, 100, SkillType.Basic)
        {
            FurySpent = 100
        };
        state.ProcessedEvents.Add(furySpentEvent);
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));

        InvigoratingFury.ProcessEvent(furySpentEvent, state);

        state.Events.Should().ContainSingle(e => e is HealingEvent);
        state.Events.OfType<HealingEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<HealingEvent>().First().BaseAmountHealed.Should().Be(30);
    }

    [Fact]
    public void Does_Not_Proc_When_Fury_Not_Crossing_100_Threshold()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.InvigoratingFury, 1);
        var previousFuryEvent = new FurySpentEvent(110, 25, SkillType.Basic)
        {
            FurySpent = 25
        };
        state.ProcessedEvents.Add(previousFuryEvent);
        var furySpentEvent = new FurySpentEvent(123, 70, SkillType.Basic)
        {
            FurySpent = 70
        };
        state.ProcessedEvents.Add(furySpentEvent);

        InvigoratingFury.ProcessEvent(furySpentEvent, state);

        state.Events.Should().NotContain(e => e is HealingEvent);
    }

    [Fact]
    public void Procs_When_Fury_Crosses_100_Threshold()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.InvigoratingFury, 1);
        var previousFuryEvent = new FurySpentEvent(110, 25, SkillType.Basic)
        {
            FurySpent = 35
        };
        state.ProcessedEvents.Add(previousFuryEvent);
        var furySpentEvent = new FurySpentEvent(123, 70, SkillType.Basic)
        {
            FurySpent = 70
        };
        state.ProcessedEvents.Add(furySpentEvent);

        InvigoratingFury.ProcessEvent(furySpentEvent, state);

        state.Events.Should().ContainSingle(e => e is HealingEvent);
    }

    [Fact]
    public void Does_Not_Proc_If_Skill_Points_Are_Zero()
    {
        var state = new SimulationState(new SimulationConfig());
        var furySpentEvent = new FurySpentEvent(123, 100, SkillType.Basic);
        BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));

        InvigoratingFury.ProcessEvent(furySpentEvent, state);

        state.Events.Should().NotContain(e => e is HealingEvent);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0.03)]
    [InlineData(2, 0.06)]
    [InlineData(3, 0.09)]
    [InlineData(4, 0.09)]
    public void Skill_Points_Determines_Percent_Proc(int skillPoints, double healingPercentage)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.InvigoratingFury, skillPoints);

        InvigoratingFury.GetHealingPercentage(state).Should().Be(healingPercentage);
    }
}
