using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class IronSkinTests
{
    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());

        IronSkin.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.IronSkinCooldown);

        IronSkin.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_IronSkinEvent()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        IronSkin.Use(state);

        state.Events.Should().ContainSingle(e => e is IronSkinEvent);
        state.Events.OfType<IronSkinEvent>().First().Timestamp.Should().Be(123);
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
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.IronSkin, skillPoints);

        IronSkin.GetBarrierPercentage(state).Should().Be(barrierPercentage);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.IronSkin, 1);
        state.Config.Gear.Helm.IronSkin = 2;

        IronSkin.GetBarrierPercentage(state).Should().Be(0.6);
    }
}
