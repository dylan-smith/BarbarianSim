using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public sealed class LungingStrikeTests
{
    [Fact]
    public void CanUse_When_Weapon_On_Cooldown_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WeaponCooldown);

        LungingStrike.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_When_Weapon_Not_On_Cooldown_Returns_True()
    {
        var state = new SimulationState(new SimulationConfig());

        LungingStrike.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void Use_Adds_LungingStrikeEvent_To_Events()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        LungingStrike.Use(state, state.Enemies.First());

        state.Events.Count.Should().Be(1);
        state.Events[0].Should().BeOfType<LungingStrikeEvent>();
        state.Events[0].Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(1, 0.33)]
    [InlineData(2, 0.36)]
    [InlineData(3, 0.39)]
    [InlineData(4, 0.42)]
    [InlineData(5, 0.45)]
    [InlineData(6, 0.45)]
    public void GetSkillMultiplier_Converts_Skill_Points_To_Correct_Multiplier(int skillPoints, double expectedMultiplier)
    {
        var state = new SimulationState(new SimulationConfig { Skills = { [Skill.LungingStrike] = skillPoints } });

        var result = LungingStrike.GetSkillMultiplier(state);

        result.Should().Be(expectedMultiplier);
    }

    [Fact]
    public void GetSkillMultiplier_Includes_Skill_Points_And_Gear_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.LungingStrike] = 1 },
        });

        state.Config.Gear.Helm.LungingStrike = 2;

        var result = LungingStrike.GetSkillMultiplier(state);

        result.Should().Be(0.39);
    }
}
