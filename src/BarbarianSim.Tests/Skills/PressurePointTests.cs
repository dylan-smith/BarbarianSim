using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class PressurePointTests
{
    private readonly Mock<RandomGenerator> _mockRandomGenerator = TestHelpers.CreateMock<RandomGenerator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly PressurePoint _skill;

    public PressurePointTests()
    {
        _mockRandomGenerator.Setup(m => m.Roll(RollType.PressurePoint)).Returns(0.0);
        _skill = new PressurePoint(_mockRandomGenerator.Object);
    }

    [Fact]
    public void Creates_PressurePointProcEvent()
    {
        _state.Config.Skills.Add(Skill.PressurePoint, 1);
        var luckyHitEvent = new LuckyHitEvent(123, null, SkillType.Core, _state.Enemies.First(), null);

        _skill.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().ContainSingle(e => e is PressurePointProcEvent);
        _state.Events.Cast<PressurePointProcEvent>().First().Timestamp.Should().Be(123);
        _state.Events.Cast<PressurePointProcEvent>().First().Target.Should().Be(_state.Enemies.First());
    }

    [Fact]
    public void Only_Procs_On_Core_Skills()
    {
        var luckyHitEvent = new LuckyHitEvent(123, null, SkillType.Basic, _state.Enemies.First(), null);

        _skill.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().NotContain(e => e is PressurePointProcEvent);
    }

    [Fact]
    public void Does_Not_Proc_If_Skill_Points_Are_Zero()
    {
        _state.Config.Skills.Add(Skill.PressurePoint, 0);
        _mockRandomGenerator.Setup(m => m.Roll(RollType.PressurePoint)).Returns(0.001);
        var luckyHitEvent = new LuckyHitEvent(123, null, SkillType.Core, _state.Enemies.First(), null);

        _skill.ProcessEvent(luckyHitEvent, _state);

        _state.Events.Should().NotContain(e => e is PressurePointProcEvent);
    }

    [Theory]
    [InlineData(1, 0.1)]
    [InlineData(2, 0.2)]
    [InlineData(3, 0.3)]
    [InlineData(4, 0.3)]
    public void Skill_Points_Determines_Percent_Proc(int skillPoints, double procRate)
    {
        _state.Config.Skills.Add(Skill.PressurePoint, skillPoints);

        _skill.GetProcPercentage(_state).Should().Be(procRate);
    }
}
