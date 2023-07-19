using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class PrimeWrathOfTheBerserkerTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly PrimeWrathOfTheBerserker _skill = new();

    [Fact]
    public void Returns_20_When_Active()
    {
        _state.Config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);

        _skill.GetMovementSpeedIncrease(_state).Should().Be(20);
    }

    [Fact]
    public void Returns_0_When_Not_Skilled()
    {
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);

        _skill.GetMovementSpeedIncrease(_state).Should().Be(0);
    }

    [Fact]
    public void Returns_20_When_No_Aura()
    {
        _state.Config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);

        _skill.GetMovementSpeedIncrease(_state).Should().Be(0);
    }

    [Fact]
    public void GetResourceGeneration_When_Active()
    {
        _state.Config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);

        _skill.GetResourceGeneration(_state).Should().Be(PrimeWrathOfTheBerserker.FURY_GENERATION_INCREASE);
    }

    [Fact]
    public void GetResourceGeneration_Returns_1_When_Not_Skilled()
    {
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);

        _skill.GetResourceGeneration(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetResourceGeneration_Returns_1_When_Not_Active()
    {
        _state.Config.Skills.Add(Skill.PrimeWrathOfTheBerserker, 1);

        _skill.GetResourceGeneration(_state).Should().Be(1.0);
    }
}
