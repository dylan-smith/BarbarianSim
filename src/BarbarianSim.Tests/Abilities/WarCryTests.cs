using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class WarCryTests
{
    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());

        WarCry.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WarCryCooldown);

        WarCry.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_WarCryEvent()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        WarCry.Use(state);

        state.Events.Should().ContainSingle(e => e is WarCryEvent);
        state.Events.OfType<WarCryEvent>().First().Timestamp.Should().Be(123);
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
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WarCry, skillPoints);

        WarCry.GetDamageBonus(state).Should().Be(damageBonus);
    }

    [Fact]
    public void Skill_Points_From_Gear_Are_Included()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WarCry, 1);
        state.Config.Gear.Helm.WarCry = 2;

        WarCry.GetDamageBonus(state).Should().Be(1.18);
    }

    [Fact]
    public void PowerWarCry_Adds_DamageBonus_When_6_Enemies_Nearby()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 6;
        var state = new SimulationState(config);
        state.Config.Skills.Add(Skill.WarCry, 1);
        state.Config.Skills.Add(Skill.PowerWarCry, 1);

        WarCry.GetDamageBonus(state).Should().Be(1.25);
    }
}
