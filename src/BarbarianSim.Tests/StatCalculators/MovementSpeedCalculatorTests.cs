﻿using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MovementSpeedCalculatorTests
{
    [Fact]
    public void Includes_Bonus_From_Gear()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Bonus_From_Berserking()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Player.Auras.Add(Aura.Berserking);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(1.27);
    }

    [Fact]
    public void Bonus_From_RallyingCry()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Player.Auras.Add(Aura.RallyingCry);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(1.42);
    }

    [Fact]
    public void Bonus_From_PrimeWrathOfTheBerserker()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(1.32);
    }

    [Fact]
    public void Bonus_From_PrimeWrathOfTheBerserker_Only_When_Skilled()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Bonus_From_PrimeWrathOfTheBerserker_Only_When_Active()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Bonus_From_Ghostwalker()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Gear.Helm.MovementSpeed = 12.0;
        state.Config.Gear.Helm.Aspect = new GhostwalkerAspect(20);
        state.Player.Auras.Add(Aura.Ghostwalker);

        var result = MovementSpeedCalculator.Calculate(state);

        result.Should().Be(1.32);
    }
}
