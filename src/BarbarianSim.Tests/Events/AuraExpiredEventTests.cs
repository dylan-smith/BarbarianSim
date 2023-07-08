using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class AuraExpiredEventTests
{
    [Fact]
    public void Removes_Aura()
    {
        var testAura = Aura.WarCry;

        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(testAura);
        var e = new AuraExpiredEvent(123.0, testAura);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(testAura);
    }

    [Fact]
    public void Leaves_Aura_If_Other_AuraExpiredEvents_Exist()
    {
        var testAura = Aura.WarCry;

        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(testAura);
        state.Events.Add(new AuraExpiredEvent(126.0, testAura));
        var e = new AuraExpiredEvent(123.0, testAura);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(testAura);
    }

    [Fact]
    public void Only_Looks_At_Other_AuraExpiredEvents_For_The_Same_Aura()
    {
        var testAura = Aura.WarCry;
        var diffAura = Aura.Whirlwinding;

        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(testAura);
        state.Events.Add(new AuraExpiredEvent(126.0, diffAura));
        var e = new AuraExpiredEvent(123.0, testAura);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(testAura);
    }

    [Fact]
    public void Only_Looks_At_Other_AuraExpiredEvents_For_The_Same_Enemy()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);

        var testEnemy = state.Enemies.First();
        var diffEnemy = state.Enemies.Last();

        testEnemy.Auras.Add(Aura.Berserking);
        state.Events.Add(new AuraExpiredEvent(126.0, Aura.Berserking));
        state.Events.Add(new AuraExpiredEvent(126.0, diffEnemy, Aura.Berserking));
        var e = new AuraExpiredEvent(123.0, testEnemy, Aura.Berserking);

        e.ProcessEvent(state);

        testEnemy.Auras.Should().NotContain(Aura.Berserking);
    }

    [Fact]
    public void Removes_Enemy_Specific_Auras()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);

        state.Enemies.First().Auras.Add(Aura.Berserking);
        state.Enemies.Last().Auras.Add(Aura.Berserking);
        var e = new AuraExpiredEvent(123.0, state.Enemies.Last(), Aura.Berserking);

        e.ProcessEvent(state);

        state.Enemies.Last().Auras.Should().NotContain(Aura.Berserking);
        state.Enemies.First().Auras.Should().Contain(Aura.Berserking);
    }
}
