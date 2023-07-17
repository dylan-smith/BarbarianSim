using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class AuraExpiredEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AuraExpiredEventHandler _handler = new();

    [Fact]
    public void Removes_Aura()
    {
        var testAura = Aura.WarCry;

        _state.Player.Auras.Add(testAura);
        var auraExpiredEvent = new AuraExpiredEvent(123.0, testAura);

        _handler.ProcessEvent(auraExpiredEvent, _state);

        _state.Player.Auras.Should().NotContain(testAura);
    }

    [Fact]
    public void Leaves_Aura_If_Other_AuraExpiredEvents_Exist()
    {
        var testAura = Aura.WarCry;

        _state.Player.Auras.Add(testAura);
        _state.Events.Add(new AuraExpiredEvent(126.0, testAura));
        var auraExpiredEvent = new AuraExpiredEvent(123.0, testAura);

        _handler.ProcessEvent(auraExpiredEvent, _state);

        _state.Player.Auras.Should().Contain(testAura);
    }

    [Fact]
    public void Only_Looks_At_Other_AuraExpiredEvents_For_The_Same_Aura()
    {
        var testAura = Aura.WarCry;
        var diffAura = Aura.Whirlwinding;

        _state.Player.Auras.Add(testAura);
        _state.Events.Add(new AuraExpiredEvent(126.0, diffAura));
        var auraExpiredEvent = new AuraExpiredEvent(123.0, testAura);

        _handler.ProcessEvent(auraExpiredEvent, _state);

        _state.Player.Auras.Should().NotContain(testAura);
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
        var auraExpiredEvent = new AuraExpiredEvent(123.0, testEnemy, Aura.Berserking);

        _handler.ProcessEvent(auraExpiredEvent, state);

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
        var auraExpiredEvent = new AuraExpiredEvent(123.0, state.Enemies.Last(), Aura.Berserking);

        _handler.ProcessEvent(auraExpiredEvent, state);

        state.Enemies.Last().Auras.Should().NotContain(Aura.Berserking);
        state.Enemies.First().Auras.Should().Contain(Aura.Berserking);
    }
}
