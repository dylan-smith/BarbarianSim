using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfTheProtectorTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly AspectOfTheProtector _aspect = new();

    [Fact]
    public void Creates_AspectOfTheProtectorProcEvent()
    {
        _state.Config.EnemySettings.IsElite = true;
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, _state.Enemies.First());
        _state.CurrentTime = 123.0;

        _aspect.BarrierAmount = 1000;

        _aspect.ProcessEvent(dmg, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfTheProtectorProcEvent);
        _state.Events.First().Timestamp.Should().Be(123.0);
        _state.Events.OfType<AspectOfTheProtectorProcEvent>().First().BarrierAmount.Should().Be(1000);
    }

    [Fact]
    public void Does_Not_Fire_If_Enemy_Not_Elite()
    {
        _state.Config.EnemySettings.IsElite = false;
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, _state.Enemies.First());

        _aspect.BarrierAmount = 1000;

        _aspect.ProcessEvent(dmg, _state);

        _state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Not_Fire_When_On_Cooldown()
    {
        _state.Config.EnemySettings.IsElite = true;
        _state.Player.Auras.Add(Aura.AspectOfTheProtectorCooldown);
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, _state.Enemies.First());

        _aspect.BarrierAmount = 1000;

        _aspect.ProcessEvent(dmg, _state);

        _state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Not_Fire_When_Already_Procced()
    {
        _state.Config.EnemySettings.IsElite = true;
        _state.Events.Add(new AspectOfTheProtectorProcEvent(0.0, 1000));
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, _state.Enemies.First());

        _aspect.BarrierAmount = 1000;

        _aspect.ProcessEvent(dmg, _state);

        _state.Events.Should().ContainSingle(e => e is AspectOfTheProtectorProcEvent);
    }
}
