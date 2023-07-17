using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class BleedCompletedEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly BleedCompletedEventHandler _handler = new();

    [Fact]
    public void Removes_Bleeding_Aura()
    {
        _state.Enemies.First().Auras.Add(Aura.Bleeding);
        var bleedCompletedEvent = new BleedCompletedEvent(123.0, 500.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedCompletedEvent, _state);

        _state.Enemies.First().Auras.Should().NotContain(Aura.Bleeding);
    }

    [Fact]
    public void Leaves_Bleeding_Aura_If_Other_BleedCompletedEvents_Exist()
    {
        _state.Enemies.First().Auras.Add(Aura.Bleeding);
        _state.Events.Add(new BleedCompletedEvent(126.0, 300.0, _state.Enemies.First()));
        var bleedCompletedEvent = new BleedCompletedEvent(123.0, 500.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedCompletedEvent, _state);

        _state.Enemies.First().Auras.Should().Contain(Aura.Bleeding);
    }

    [Fact]
    public void Creates_DamageEvent()
    {
        var bleedCompletedEvent = new BleedCompletedEvent(123.0, 500.0, _state.Enemies.First());

        _handler.ProcessEvent(bleedCompletedEvent, _state);

        _state.Events.Should().Contain(bleedCompletedEvent.DamageEvent);
        _state.Events.Should().ContainSingle(e => e is DamageEvent);
        bleedCompletedEvent.DamageEvent.Timestamp.Should().Be(123.0);
        bleedCompletedEvent.DamageEvent.Damage.Should().Be(500.0);
        bleedCompletedEvent.DamageEvent.DamageType.Should().Be(DamageType.DamageOverTime);
        bleedCompletedEvent.DamageEvent.DamageSource.Should().Be(DamageSource.Bleeding);
        bleedCompletedEvent.DamageEvent.Target.Should().Be(_state.Enemies.First());
        bleedCompletedEvent.DamageEvent.SkillType.Should().Be(SkillType.None);
    }
}
