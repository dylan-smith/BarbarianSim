using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class ChallengingShoutTests
{
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly ChallengingShout _challengingShout;

    public ChallengingShoutTests()
    {
        _challengingShout = new(_mockSimLogger.Object);
    }

    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.ChallengingShout, 1);
        _challengingShout.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.ChallengingShout, 1);
        _state.Player.Auras.Add(Aura.ChallengingShoutCooldown);

        _challengingShout.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_Returns_False_If_Not_Skilled()
    {
        _challengingShout.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_ChallengingShoutEvent()
    {
        _state.CurrentTime = 123;

        _challengingShout.Use(_state);

        _state.Events.Should().ContainSingle(e => e is ChallengingShoutEvent);
        _state.Events.Cast<ChallengingShoutEvent>().First().Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 40)]
    [InlineData(2, 42)]
    [InlineData(3, 44)]
    [InlineData(4, 46)]
    [InlineData(5, 48)]
    [InlineData(6, 48)]
    public void Skill_Points_Determines_ResourceGeneration(int skillPoints, double damageReduction)
    {
        _state.Config.Skills.Add(Skill.ChallengingShout, skillPoints);

        _challengingShout.GetDamageReduction(_state).Should().Be(damageReduction);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included_In_When_Calculating_Damage_Reduction()
    {
        _state.Config.Skills.Add(Skill.ChallengingShout, 1);
        _state.Config.Gear.Helm.ChallengingShout = 2;

        _challengingShout.GetDamageReduction(_state).Should().Be(44);
    }
}
