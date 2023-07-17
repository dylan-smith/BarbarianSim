using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class WarCryTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WarCry _warCry = new();

    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        _warCry.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        _state.Player.Auras.Add(Aura.WarCryCooldown);

        _warCry.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_WarCryEvent()
    {
        _state.CurrentTime = 123;

        _warCry.Use(_state);

        _state.Events.Should().ContainSingle(e => e is WarCryEvent);
        _state.Events.OfType<WarCryEvent>().First().Timestamp.Should().Be(123);
    }

    [Theory]
    [InlineData(0, 1.0)]
    [InlineData(1, 1.15)]
    [InlineData(2, 1.165)]
    [InlineData(3, 1.18)]
    [InlineData(4, 1.195)]
    [InlineData(5, 1.21)]
    [InlineData(6, 1.21)]
    public void Skill_Points_Determines_DamageBonus(int skillPoints, double damageBonus)
    {
        _state.Config.Skills.Add(Skill.WarCry, skillPoints);

        _warCry.GetDamageBonus(_state).Should().Be(damageBonus);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included()
    {
        _state.Config.Skills.Add(Skill.WarCry, 1);
        _state.Config.Gear.Helm.WarCry = 2;

        _warCry.GetDamageBonus(_state).Should().Be(1.18);
    }

    [Fact]
    public void PowerWarCry_Adds_DamageBonus_When_6_Enemies_Nearby()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 6;
        var state = new SimulationState(config);
        state.Config.Skills.Add(Skill.WarCry, 1);
        state.Config.Skills.Add(Skill.PowerWarCry, 1);

        _warCry.GetDamageBonus(state).Should().Be(1.25);
    }

    [Fact]
    public void PowerWarCry_Does_Nothing_When_Less_Than_6_Enemies_Nearby()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 5;
        var state = new SimulationState(config);
        state.Config.Skills.Add(Skill.WarCry, 1);
        state.Config.Skills.Add(Skill.PowerWarCry, 1);

        _warCry.GetDamageBonus(state).Should().Be(1.15);
    }
}
