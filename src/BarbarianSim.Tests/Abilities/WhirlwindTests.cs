﻿using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public sealed class WhirlwindTests : IDisposable
{
    public void Dispose()
    {
        BaseStatCalculator.ClearMocks();
    }

    public WhirlwindTests()
    {
        BaseStatCalculator.InjectMock(typeof(FuryCostReductionCalculator), new FakeStatCalculator(1.0, SkillType.Core));
    }

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
    public void CanUse_Considers_FuryCostReduction()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 20;
        BaseStatCalculator.InjectMock(typeof(FuryCostReductionCalculator), new FakeStatCalculator(0.8, SkillType.Core));

        Whirlwind.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanRefresh_When_Enough_Fury_And_Whirlwinding_Aura_Applied_Should_Return_True()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 25;
        state.Player.Auras.Add(Aura.Whirlwinding);

        Whirlwind.CanRefresh(state).Should().BeTrue();
    }

    [Fact]
    public void CanRefresh_When_Not_Enough_Fury_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 24;
        state.Player.Auras.Add(Aura.Whirlwinding);

        Whirlwind.CanRefresh(state).Should().BeFalse();
    }

    [Fact]
    public void CanRefresh_When_No_Whirlwinding_Aura_Returns_False()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 100;

        Whirlwind.CanRefresh(state).Should().BeFalse();
    }

    [Fact]
    public void CanRefresh_Considers_FuryCostReduction()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Fury = 20;
        state.Player.Auras.Add(Aura.Whirlwinding);
        BaseStatCalculator.InjectMock(typeof(FuryCostReductionCalculator), new FakeStatCalculator(0.8, SkillType.Core));

        Whirlwind.CanRefresh(state).Should().BeTrue();
    }

    [Fact]
    public void Use_Adds_WhirlwindSpinEvent_To_Events()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        Whirlwind.Use(state);

        state.Events.Count.Should().Be(1);
        state.Events[0].Should().BeOfType<WhirlwindSpinEvent>();
        state.Events[0].Timestamp.Should().Be(123);
    }

    [Fact]
    public void StopSpinning_Creates_AuraExpiredEvent()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        Whirlwind.StopSpinning(state);

        state.Events.Should().ContainSingle(e => e is AuraExpiredEvent);
        state.Events.OfType<AuraExpiredEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<AuraExpiredEvent>().First().Aura.Should().Be(Aura.Whirlwinding);
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
