using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class StrategicRallyingCryTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly StrategicRallyingCry _skill = new();

    [Fact]
    public void Creates_FortifyGeneratedEvent()
    {
        _state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        _state.Player.BaseLife = 4000;
        var rallyingCryEvent = new RallyingCryEvent(123);

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Timestamp.Should().Be(123);
        _state.Events.OfType<FortifyGeneratedEvent>().Single().Amount.Should().Be(400);
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        _state.Config.Skills.Add(Skill.StrategicRallyingCry, 0);
        _state.Player.BaseLife = 4000;
        var rallyingCryEvent = new RallyingCryEvent(123);

        _skill.ProcessEvent(rallyingCryEvent, _state);

        _state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }

    [Fact]
    public void StrategicRallyingCry_Creates_FortifyGeneratedEvent_On_Direct_Damage()
    {
        _state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        _state.Player.Auras.Add(Aura.RallyingCry);
        _state.Player.BaseLife = 4000;
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct | DamageType.CriticalStrike, DamageSource.Whirlwind, SkillType.Core, 0, Expertise.Polearm, _state.Enemies.First());

        _skill.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is FortifyGeneratedEvent);
        _state.Events.OfType<FortifyGeneratedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<FortifyGeneratedEvent>().First().Amount.Should().Be(80);
    }

    [Fact]
    public void StrategicRallyingCry_Does_Not_Fortify_If_Missing_Skill()
    {
        _state.Player.Auras.Add(Aura.RallyingCry);
        _state.Player.BaseLife = 4000;
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.Whirlwind, SkillType.Core, 0, Expertise.Polearm, _state.Enemies.First());

        _skill.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }

    [Fact]
    public void StrategicRallyingCry_Does_Not_Fortify_If_Shout_Not_Active()
    {
        _state.Config.Skills.Add(Skill.StrategicRallyingCry, 1);
        _state.Player.BaseLife = 4000;
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.Whirlwind, SkillType.Core, 0, Expertise.Polearm, _state.Enemies.First());

        _skill.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is FortifyGeneratedEvent);
    }
}
