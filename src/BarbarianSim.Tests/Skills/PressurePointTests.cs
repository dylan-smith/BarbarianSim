using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public sealed class PressurePointTests : IDisposable
{
    public void Dispose()
    {
        RandomGenerator.ClearMock();
        BaseStatCalculator.ClearMocks();
    }

    [Fact]
    public void Creates_PressurePointProcEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.PressurePoint, 1);
        RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.PressurePoint, 0.0));
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.Core, state.Enemies.First());

        PressurePoint.ProcessEvent(luckyHitEvent, state);

        state.Events.Should().ContainSingle(e => e is PressurePointProcEvent);
        state.Events.Cast<PressurePointProcEvent>().First().Timestamp.Should().Be(123);
        state.Events.Cast<PressurePointProcEvent>().First().Target.Should().Be(state.Enemies.First());
    }

    [Fact]
    public void Only_Procs_On_Core_Skills()
    {
        var state = new SimulationState(new SimulationConfig());
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.Basic, state.Enemies.First());

        PressurePoint.ProcessEvent(luckyHitEvent, state);

        state.Events.Should().NotContain(e => e is PressurePointProcEvent);
    }

    [Fact]
    public void Does_Not_Proc_If_Skill_Points_Are_Zero()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.PressurePoint, 0);
        var luckyHitEvent = new LuckyHitEvent(123, SkillType.Core, state.Enemies.First());

        PressurePoint.ProcessEvent(luckyHitEvent, state);

        state.Events.Should().NotContain(e => e is PressurePointProcEvent);
    }

    [Theory]
    [InlineData(1, 0.1)]
    [InlineData(2, 0.2)]
    [InlineData(3, 0.3)]
    [InlineData(4, 0.3)]
    public void Skill_Points_Determines_Percent_Proc(int skillPoints, double procRate)
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.PressurePoint, skillPoints);

        PressurePoint.GetProcPercentage(state).Should().Be(procRate);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.PressurePoint, 1);
        state.Config.Gear.Helm.PressurePoint = 2;

        PressurePoint.GetProcPercentage(state).Should().Be(0.3);
    }
}
