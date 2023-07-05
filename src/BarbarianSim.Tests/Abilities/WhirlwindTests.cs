using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public sealed class WhirlwindTests
{
    [Fact]
    public void CanUse_Returns_True_When_Enough_Fury()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 25;

        Whirlwind.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_When_Weapon_On_Cooldown_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 25;
        state.Player.Auras.Add(Aura.WeaponCooldown);

        Whirlwind.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_When_Whirlwinding_Aura_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 25;
        state.Player.Auras.Add(Aura.Whirlwinding);

        Whirlwind.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_When_Not_Enough_Fury_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 24;

        Whirlwind.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void CanRefresh_When_Not_Enough_Fury_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 24;

        Whirlwind.CanRefresh(state).Should().BeFalse();
    }

    [Fact]
    public void CanRefresh_When_Enough_Fury_Returns_True()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 25;

        Whirlwind.CanRefresh(state).Should().BeTrue();
    }

    [Fact]
    public void Use_Adds_WhirlwindStartedEvent_To_Events()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        Whirlwind.Use(state);

        state.Events.Count.Should().Be(1);
        state.Events[0].Should().BeOfType<WhirlwindStartedEvent>();
        state.Events[0].Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(1, 0.17)]
    [InlineData(2, 0.19)]
    [InlineData(3, 0.21)]
    [InlineData(4, 0.23)]
    [InlineData(5, 0.24)]
    [InlineData(6, 0.24)]
    public void GetSkillMultiplier_Converts_Skill_Points_To_Correct_Multiplier(int skillPoints, double expectedMultiplier)
    {
        var state = new SimulationState(new SimulationConfig { Skills = { [Skill.Whirlwind] = skillPoints } });

        var result = Whirlwind.GetSkillMultiplier(state);

        result.Should().Be(expectedMultiplier);
    }

    [Fact]
    public void GetSkillMultiplier_Includes_Skill_Points_And_Gear_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig
        {
            Skills = { [Skill.Whirlwind] = 1 },
        });

        state.Config.Gear.Helm.Whirlwind = 2;

        var result = Whirlwind.GetSkillMultiplier(state);

        result.Should().Be(0.21);
    }
}
