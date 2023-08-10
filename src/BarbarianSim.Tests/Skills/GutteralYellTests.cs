using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class GutteralYellTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly GutteralYell _skill;

    public GutteralYellTests() => _skill = new(_mockSimLogger.Object);

    [Fact]
    public void Creates_ProcEvent_On_WarCry()
    {
        var shoutEvent = new WarCryEvent(123);

        _skill.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is GutteralYellProcEvent);
        _state.Events.OfType<GutteralYellProcEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_ProcEvent_On_ChallengingShout()
    {
        var shoutEvent = new ChallengingShoutEvent(123);

        _skill.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is GutteralYellProcEvent);
        _state.Events.OfType<GutteralYellProcEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_ProcEvent_On_RallyingCry()
    {
        var shoutEvent = new RallyingCryEvent(123);

        _skill.ProcessEvent(shoutEvent, _state);

        _state.Events.Should().ContainSingle(e => e is GutteralYellProcEvent);
        _state.Events.OfType<GutteralYellProcEvent>().First().Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 4)]
    [InlineData(2, 8)]
    [InlineData(3, 12)]
    [InlineData(4, 12)]
    public void Skill_Points_Determines_Percent_Proc(int skillPoints, double damageReduction)
    {
        _state.Config.Skills.Add(Skill.GutteralYell, skillPoints);

        _skill.GetDamageReduction(_state).Should().Be(damageReduction);
    }
}
