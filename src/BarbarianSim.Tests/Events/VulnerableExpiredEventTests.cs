using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class VulnerableExpiredEventTests
{
    [Fact]
    public void Removes_Vulnerable_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemies.First().Auras.Add(Aura.Vulnerable);

        var e = new VulnerableExpiredEvent(123.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Enemies.First().Auras.Should().NotContain(Aura.Vulnerable);
    }

    [Fact]
    public void Leaves_Aura_If_Other_Vulnerable_Timers_Exist_On_Same_Target()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemies.First().Auras.Add(Aura.Vulnerable);
        state.Events.Add(new VulnerableExpiredEvent(124, state.Enemies.First()));

        var e = new VulnerableExpiredEvent(123.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Enemies.First().Auras.Should().Contain(Aura.Vulnerable);
    }

    [Fact]
    public void Ignores_Vulnerable_Timers_On_Different_Enemies()
    {
        var config = new SimulationConfig();
        config.EnemySettings.NumberOfEnemies = 2;
        var state = new SimulationState(config);
        state.Enemies.First().Auras.Add(Aura.Vulnerable);
        state.Enemies.Last().Auras.Add(Aura.Vulnerable);
        state.Events.Add(new VulnerableExpiredEvent(124, state.Enemies.Last()));

        var e = new VulnerableExpiredEvent(123.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Enemies.First().Auras.Should().NotContain(Aura.Vulnerable);
    }
}
