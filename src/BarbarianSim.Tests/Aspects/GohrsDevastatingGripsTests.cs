using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class GohrsDevastatingGripsTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly GohrsDevastatingGrips _aspect = new();

    public GohrsDevastatingGripsTests()
    {
        _aspect.DamagePercent = 20;
        _state.Config.Gear.Helm.Aspect = _aspect;
    }

    [Fact]
    public void Increments_HitCount_Every_DirectDamageEvent_From_Whirlwind()
    {
        _aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, null, _state.Enemies.First()), _state);
        _aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0.0, null, _state.Enemies.First()), _state);
        _aspect.ProcessEvent(new DirectDamageEvent(127.0, 300.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, null, _state.Enemies.First()), _state);

        _aspect.HitCount.Should().Be(2);
    }

    [Fact]
    public void Tracks_TotalBaseDamage_Every_DirectDamageEvent_From_Whirlwind()
    {
        _aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, null, _state.Enemies.First()), _state);
        _aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0.0, null, _state.Enemies.First()), _state);
        _aspect.ProcessEvent(new DirectDamageEvent(127.0, 300.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, null, _state.Enemies.First()), _state);

        _aspect.TotalBaseDamage.Should().Be(1500.0);
    }

    [Fact]
    public void Ignores_DamageEvent_When_At_Max_HitCount()
    {
        _aspect.HitCount = 99;

        _aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, null, _state.Enemies.First()), _state);
        _aspect.ProcessEvent(new DirectDamageEvent(127.0, 300.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, null, _state.Enemies.First()), _state);

        _aspect.TotalBaseDamage.Should().Be(1200.0);
    }

    [Fact]
    public void Creates_GohrsDevastatingGripsProcEvent()
    {
        _aspect.TotalBaseDamage = 1200;

        _aspect.ProcessEvent(new WhirlwindStoppedEvent(123.0), _state);

        _state.Events.Should().ContainSingle(e => e is GohrsDevastatingGripsProcEvent);
        _state.Events.Cast<GohrsDevastatingGripsProcEvent>().First().Timestamp.Should().Be(123.0);
        _state.Events.Cast<GohrsDevastatingGripsProcEvent>().First().Damage.Should().Be(240.0);
    }

    [Fact]
    public void Does_Nothing_When_Not_Equipped()
    {
        _state.Config.Gear.Helm.Aspect = null;
        _aspect.TotalBaseDamage = 1200;

        _aspect.ProcessEvent(new WhirlwindStoppedEvent(123.0), _state);

        _state.Events.Should().NotContain(e => e is GohrsDevastatingGripsProcEvent);
    }

    [Fact]
    public void Resets_HitCount_And_TotalBaseDamage_On_Proc()
    {
        _aspect.TotalBaseDamage = 1200;
        _aspect.HitCount = 12;

        _aspect.ProcessEvent(new WhirlwindStoppedEvent(123.0), _state);

        _aspect.HitCount.Should().Be(0);
        _aspect.TotalBaseDamage.Should().Be(0);
    }
}
