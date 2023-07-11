using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class EnhancedWhirlwindTests
{
    [Fact]
    public void Generates_1_Fury_For_Non_Elites()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedWhirlwind, 1);

        var damageEvent = new DamageEvent(123.0, 2000, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, state.Enemies.First());

        EnhancedWhirlwind.ProcessEvent(damageEvent, state);

        state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        state.Events.OfType<FuryGeneratedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<FuryGeneratedEvent>().First().BaseFury.Should().Be(1);
    }

    [Fact]
    public void Generates_4_Fury_For_Elites()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.EnhancedWhirlwind, 1);
        state.Config.EnemySettings.IsElite = true;

        var damageEvent = new DamageEvent(123.0, 2000, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, state.Enemies.First());

        EnhancedWhirlwind.ProcessEvent(damageEvent, state);

        state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        state.Events.OfType<FuryGeneratedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<FuryGeneratedEvent>().First().BaseFury.Should().Be(4);
    }
}
