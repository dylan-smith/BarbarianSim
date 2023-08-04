using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class EnhancedWhirlwindTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly EnhancedWhirlwind _skill = new();

    [Fact]
    public void Generates_1_Fury_For_Non_Elites()
    {
        _state.Config.Skills.Add(Skill.EnhancedWhirlwind, 1);

        var damageEvent = new DamageEvent(123.0, null, 2000, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, _state.Enemies.First());

        _skill.ProcessEvent(damageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        _state.Events.OfType<FuryGeneratedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<FuryGeneratedEvent>().First().BaseFury.Should().Be(1);
    }

    [Fact]
    public void Generates_4_Fury_For_Elites()
    {
        _state.Config.Skills.Add(Skill.EnhancedWhirlwind, 1);
        _state.Config.EnemySettings.IsElite = true;

        var damageEvent = new DamageEvent(123.0, null, 2000, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, _state.Enemies.First());

        _skill.ProcessEvent(damageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FuryGeneratedEvent);
        _state.Events.OfType<FuryGeneratedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<FuryGeneratedEvent>().First().BaseFury.Should().Be(4);
    }
}
