using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class ChallengingShoutTests
{
    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());

        ChallengingShout.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ChallengingShoutCooldown);

        ChallengingShout.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_ChallengingShoutEvent()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        ChallengingShout.Use(state);

        state.Events.Should().ContainSingle(e => e is ChallengingShoutEvent);
        state.Events.Cast<ChallengingShoutEvent>().First().Timestamp.Should().Be(123);
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
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.ChallengingShout, skillPoints);

        ChallengingShout.GetDamageReduction(state).Should().Be(damageReduction);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included_In_When_Calculating_Damage_Reduction()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.ChallengingShout, 1);
        state.Config.Gear.Helm.ChallengingShout = 2;

        ChallengingShout.GetDamageReduction(state).Should().Be(44);
    }
}
