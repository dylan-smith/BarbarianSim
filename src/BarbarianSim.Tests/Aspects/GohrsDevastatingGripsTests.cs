using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public class GohrsDevastatingGripsTests
{
    [Fact]
    public void Increments_HitCount_Every_DirectDamageEvent_From_Whirlwind()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspect = new GohrsDevastatingGrips(20.0);

        aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, Expertise.Polearm, state.Enemies.First()), state);
        aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0.0, Expertise.Polearm, state.Enemies.First()), state);
        aspect.ProcessEvent(new DirectDamageEvent(127.0, 300.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, Expertise.Polearm, state.Enemies.First()), state);

        aspect.HitCount.Should().Be(2);
    }

    [Fact]
    public void Tracks_TotalBaseDamage_Every_DirectDamageEvent_From_Whirlwind()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspect = new GohrsDevastatingGrips(20.0);

        aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, Expertise.Polearm, state.Enemies.First()), state);
        aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, 0.0, Expertise.Polearm, state.Enemies.First()), state);
        aspect.ProcessEvent(new DirectDamageEvent(127.0, 300.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, Expertise.Polearm, state.Enemies.First()), state);

        aspect.TotalBaseDamage.Should().Be(1500.0);
    }

    [Fact]
    public void Ignores_DamageEvent_When_At_Max_HitCount()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspect = new GohrsDevastatingGrips(20.0)
        {
            HitCount = 99,
        };

        aspect.ProcessEvent(new DirectDamageEvent(123.0, 1200.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, Expertise.Polearm, state.Enemies.First()), state);
        aspect.ProcessEvent(new DirectDamageEvent(127.0, 300.0, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, 0.0, Expertise.Polearm, state.Enemies.First()), state);

        aspect.TotalBaseDamage.Should().Be(1200.0);
    }

    [Fact]
    public void Creates_GohrsDevastatingGripsProcEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspect = new GohrsDevastatingGrips(20.0)
        {
            TotalBaseDamage = 1200.0,
        };

        aspect.ProcessEvent(new WhirlwindStoppedEvent(123.0), state);

        state.Events.Should().ContainSingle(e => e is GohrsDevastatingGripsProcEvent);
        state.Events.Cast<GohrsDevastatingGripsProcEvent>().First().Timestamp.Should().Be(123.0);
        state.Events.Cast<GohrsDevastatingGripsProcEvent>().First().Damage.Should().Be(240.0);
    }

    [Fact]
    public void Resets_HitCount_And_TotalBaseDamage_On_Proc()
    {
        var state = new SimulationState(new SimulationConfig());
        var aspect = new GohrsDevastatingGrips(20.0)
        {
            TotalBaseDamage = 1200.0,
            HitCount = 12,
        };

        aspect.ProcessEvent(new WhirlwindStoppedEvent(123.0), state);

        aspect.HitCount.Should().Be(0);
        aspect.TotalBaseDamage.Should().Be(0);
    }
}
