using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class PowerWarCryTests
{
    private readonly PowerWarCry _skill = new();

    [Fact]
    public void GetDamageBonus_When_Active()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 6;
        var state = new SimulationState(config);
        state.Config.Skills.Add(Skill.PowerWarCry, 1);
        state.Player.Auras.Add(Aura.WarCry);

        _skill.GetDamageBonus(state).Should().Be(1.1);
    }

    [Fact]
    public void GetDamageBonus_Returns_0_When_Not_Skilled()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 6;
        var state = new SimulationState(config);
        state.Player.Auras.Add(Aura.WarCry);

        _skill.GetDamageBonus(state).Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Returns_0_When_No_Aura()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 6;
        var state = new SimulationState(config);
        state.Config.Skills.Add(Skill.PowerWarCry, 1);

        _skill.GetDamageBonus(state).Should().Be(1.0);
    }

    [Fact]
    public void GetDamageBonus_Returns_0_When_Not_Enough_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 5;
        var state = new SimulationState(config);
        state.Config.Skills.Add(Skill.PowerWarCry, 1);
        state.Player.Auras.Add(Aura.WarCry);

        _skill.GetDamageBonus(state).Should().Be(1.0);
    }
}
