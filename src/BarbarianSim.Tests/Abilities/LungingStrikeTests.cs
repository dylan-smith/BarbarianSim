using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public sealed class LungingStrikeTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly LungingStrike _lungingStrike = new();

    [Fact]
    public void CanUse_When_Weapon_On_Cooldown_Returns_False()
    {
        _state.Player.Auras.Add(Aura.WeaponCooldown);

        _lungingStrike.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void CanUse_When_Weapon_Not_On_Cooldown_Returns_True()
    {
        _lungingStrike.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void Use_Adds_LungingStrikeEvent_To_Events()
    {
        _state.CurrentTime = 123;

        _lungingStrike.Use(_state, _state.Enemies.First());

        _state.Events.Count.Should().Be(1);
        _state.Events[0].Should().BeOfType<LungingStrikeEvent>();
        _state.Events[0].Timestamp.Should().Be(123);
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
        _state.Config.Skills.Add(Skill.LungingStrike, skillPoints);

        var result = _lungingStrike.GetSkillMultiplier(_state);

        result.Should().Be(expectedMultiplier);
    }
}
