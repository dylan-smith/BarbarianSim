using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class IronSkinTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly IronSkin _ironSkin = new();

    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.IronSkin, 1);
        _ironSkin.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        _state.Config.Skills.Add(Skill.IronSkin, 1);
        _state.Player.Auras.Add(Aura.IronSkinCooldown);

        _ironSkin.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_Returns_False_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.IronSkin, 0);
        _ironSkin.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_IronSkinEvent()
    {
        _state.CurrentTime = 123;

        _ironSkin.Use(_state);

        _state.Events.Should().ContainSingle(e => e is IronSkinEvent);
        _state.Events.OfType<IronSkinEvent>().First().Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0.50)]
    [InlineData(2, 0.55)]
    [InlineData(3, 0.60)]
    [InlineData(4, 0.65)]
    [InlineData(5, 0.70)]
    [InlineData(6, 0.70)]
    public void Skill_Points_Determines_BarrierPercentage(int skillPoints, double barrierPercentage)
    {
        _state.Config.Skills.Add(Skill.IronSkin, skillPoints);

        _ironSkin.GetBarrierPercentage(_state).Should().Be(barrierPercentage);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included()
    {
        _state.Config.Skills.Add(Skill.IronSkin, 1);
        _state.Config.Gear.Helm.IronSkin = 2;

        _ironSkin.GetBarrierPercentage(_state).Should().Be(0.6);
    }
}
