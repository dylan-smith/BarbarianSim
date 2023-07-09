using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Events;

public class BleedCompletedEventTests
{
    [Fact]
    public void Removes_Bleeding_Aura()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemies.First().Auras.Add(Aura.Bleeding);
        var e = new BleedCompletedEvent(123.0, 500.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Enemies.First().Auras.Should().NotContain(Aura.Bleeding);
    }

    [Fact]
    public void Leaves_Bleeding_Aura_If_Other_BleedCompletedEvents_Exist()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Enemies.First().Auras.Add(Aura.Bleeding);
        state.Events.Add(new BleedCompletedEvent(126.0, 300.0, state.Enemies.First()));
        var e = new BleedCompletedEvent(123.0, 500.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Enemies.First().Auras.Should().Contain(Aura.Bleeding);
    }

    [Fact]
    public void Creates_DamageEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var e = new BleedCompletedEvent(123.0, 500.0, state.Enemies.First());

        e.ProcessEvent(state);

        state.Events.Should().Contain(e.DamageEvent);
        state.Events.Should().ContainSingle(e => e is DamageEvent);
        e.DamageEvent.Timestamp.Should().Be(123.0);
        e.DamageEvent.Damage.Should().Be(500.0);
        e.DamageEvent.DamageType.Should().Be(DamageType.DamageOverTime);
        e.DamageEvent.DamageSource.Should().Be(DamageSource.Bleeding);
        e.DamageEvent.Target.Should().Be(state.Enemies.First());
    }
}
