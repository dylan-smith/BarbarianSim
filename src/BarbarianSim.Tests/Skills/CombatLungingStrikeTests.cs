using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Skills;

public class CombatLungingStrikeTests
{
    [Fact]
    public void Grants_Berserking_On_Crit()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.CombatLungingStrike, 1);
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        CombatLungingStrike.ProcessEvent(damageEvent, state);

        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(1.5);
        state.Events.OfType<AuraAppliedEvent>().First().Aura.Should().Be(Aura.Berserking);
    }

    [Fact]
    public void Does_Nothing_On_Non_Crit()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.CombatLungingStrike, 1);
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        CombatLungingStrike.ProcessEvent(damageEvent, state);

        state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Nothing_If_Not_Skilled()
    {
        var state = new SimulationState(new SimulationConfig());
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        CombatLungingStrike.ProcessEvent(damageEvent, state);

        state.Events.Should().BeEmpty();
    }

    [Fact]
    public void Does_Nothing_If_Source_Not_LungingStrike()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.CombatLungingStrike, 1);
        var damageEvent = new DamageEvent(123, 1200, DamageType.Physical | DamageType.Direct | DamageType.CriticalStrike, DamageSource.Whirlwind, SkillType.Basic, state.Enemies.First());

        CombatLungingStrike.ProcessEvent(damageEvent, state);

        state.Events.Should().BeEmpty();
    }
}
