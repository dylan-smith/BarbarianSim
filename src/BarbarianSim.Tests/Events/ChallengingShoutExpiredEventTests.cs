using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class ChallengingShoutExpiredEventTests
{
    [Fact]
    public void Removes_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ChallengingShout);
        var e = new ChallengingShoutExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().NotContain(Aura.ChallengingShout);
    }

    [Fact]
    public void Leaves_ChallengingShout_Aura_If_Other_ChallengingShoutExpiredEvents_Exist()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.ChallengingShout);
        state.Events.Add(new ChallengingShoutExpiredEvent(126.0));
        var e = new ChallengingShoutExpiredEvent(123.0);

        e.ProcessEvent(state);

        state.Player.Auras.Should().Contain(Aura.ChallengingShout);
    }

    [Fact]
    public void Removes_Taunt_Aura_From_All_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Add(Aura.Taunt);
        }

        var e = new ChallengingShoutExpiredEvent(123.0);

        e.ProcessEvent(state);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Should().NotContain(Aura.Taunt);
        }
    }

    [Fact]
    public void Leaves_Taunt_Aura_If_Other_ChallengingShoultExpiredEvents_Exist()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Add(Aura.Taunt);
        }

        state.Events.Add(new ChallengingShoutExpiredEvent(126.0));
        var e = new ChallengingShoutExpiredEvent(123.0);

        e.ProcessEvent(state);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Should().Contain(Aura.Taunt);
        }
    }
}
