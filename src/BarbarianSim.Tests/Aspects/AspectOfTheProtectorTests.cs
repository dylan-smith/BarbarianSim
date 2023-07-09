using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Aspects;

public sealed class AspectOfTheProtectorTests
{
    [Fact]
    public void Creates_AspectOfTheProtectorProcEvent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.EnemySettings.IsElite = true;
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());
        state.CurrentTime = 123.0;

        var aspect = new AspectOfTheProtector(1000);

        aspect.ProcessEvent(dmg, state);

        state.Events.Should().ContainSingle(e => e is AspectOfTheProtectorProcEvent);
        state.Events.First().Timestamp.Should().Be(123.0);
        (state.Events.First() as AspectOfTheProtectorProcEvent).BarrierAmount.Should().Be(1000);
    }

    [Fact]
    public void Does_Not_Fire_If_Enemy_Not_Elite()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.EnemySettings.IsElite = false;
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        var aspect = new AspectOfTheProtector(1000);

        aspect.ProcessEvent(dmg, state);

        state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Not_Fire_When_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.EnemySettings.IsElite = true;
        state.Player.Auras.Add(Aura.AspectOfTheProtectorCooldown);
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        var aspect = new AspectOfTheProtector(1000);

        aspect.ProcessEvent(dmg, state);

        state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Not_Fire_When_Already_Procced()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.EnemySettings.IsElite = true;
        state.Events.Add(new AspectOfTheProtectorProcEvent(0.0, 1000));
        var dmg = new DamageEvent(0.0, 1.0, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        var aspect = new AspectOfTheProtector(1000);

        aspect.ProcessEvent(dmg, state);

        state.Events.Should().ContainSingle(e => e is AspectOfTheProtectorProcEvent);
    }
}
