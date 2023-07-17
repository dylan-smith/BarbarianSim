using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class CombatLungingStrikeTests
{
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly CombatLungingStrike _skill = new();

    [Fact]
    public void Grants_Berserking_On_Crit()
    {
        _state.Config.Skills.Add(Skill.CombatLungingStrike, 1);
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike, DamageSource.LungingStrike, SkillType.Basic, _state.Enemies.First());

        _skill.ProcessEvent(damageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(1.5);
        _state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Berserking);
    }

    [Fact]
    public void Does_Nothing_On_Non_Crit()
    {
        _state.Config.Skills.Add(Skill.CombatLungingStrike, 1);
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, _state.Enemies.First());

        _skill.ProcessEvent(damageEvent, _state);

        _state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike, DamageSource.LungingStrike, SkillType.Basic, _state.Enemies.First());

        _skill.ProcessEvent(damageEvent, _state);

        _state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Nothing_If_Source_Not_LungingStrike()
    {
        _state.Config.Skills.Add(Skill.CombatLungingStrike, 1);
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike, DamageSource.Whirlwind, SkillType.Basic, _state.Enemies.First());

        _skill.ProcessEvent(damageEvent, _state);

        _state.Events.Should().BeEmpty();
    }
}
