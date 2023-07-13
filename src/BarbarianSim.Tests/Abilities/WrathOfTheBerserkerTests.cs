using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class WrathOfTheBerserkerTests
{
    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());

        WrathOfTheBerserker.CanUse(state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WrathOfTheBerserkerCooldown);

        WrathOfTheBerserker.CanUse(state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_WrathOfTheBerserkerEvent()
    {
        var state = new SimulationState(new SimulationConfig())
        {
            CurrentTime = 123
        };

        WrathOfTheBerserker.Use(state);

        state.Events.Should().ContainSingle(e => e is WrathOfTheBerserkerEvent);
        state.Events.OfType<WrathOfTheBerserkerEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_BerserkingAppliedEvent_On_Direct_Damage_With_Basic_Skills()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.Direct | DamageType.CriticalStrike, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(5);
    }

    [Fact]
    public void Does_Not_Berserk_If_Missing_Skill()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void Does_Not_Berserk_If_Core_Skill()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Core, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void Does_Not_Berserk_If_Bleeding_Damage()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var damageEvent = new DamageEvent(123, 500, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void Does_Not_Berserk_If_Aura_Missing()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        var damageEvent = new DamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, state.Enemies.First());

        WrathOfTheBerserker.ProcessEvent(damageEvent, state);

        state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_3x_When_157_Fury_Spent()
    {
        var state = new SimulationState(new SimulationConfig());
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Player.Auras.Add(Aura.Berserking);
        state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        WrathOfTheBerserker.GetBerserkDamageBonus(state).Should().Be(1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Sums_All_FurySpentEvents()
    {
        var state = new SimulationState(new SimulationConfig());
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(123.5, 46, SkillType.None) { FurySpent = 46 });
        state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Player.Auras.Add(Aura.Berserking);
        state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        WrathOfTheBerserker.GetBerserkDamageBonus(state).Should().Be(1.25 * 1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Excludes_Fury_Spent_Before_Wrath()
    {
        var state = new SimulationState(new SimulationConfig());
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(122, 157, SkillType.None) { FurySpent = 157 });
        state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Player.Auras.Add(Aura.Berserking);
        state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        WrathOfTheBerserker.GetBerserkDamageBonus(state).Should().Be(1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_FurySpent_Less_Than_50()
    {
        var state = new SimulationState(new SimulationConfig());
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(123.5, 12, SkillType.None) { FurySpent = 12 });
        state.ProcessedEvents.Add(new FurySpentEvent(127, 25, SkillType.None) { FurySpent = 25 });
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Player.Auras.Add(Aura.Berserking);
        state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        WrathOfTheBerserker.GetBerserkDamageBonus(state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Wrath_Not_Active()
    {
        var state = new SimulationState(new SimulationConfig());
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        state.Player.Auras.Add(Aura.Berserking);
        state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        WrathOfTheBerserker.GetBerserkDamageBonus(state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Not_Berserking()
    {
        var state = new SimulationState(new SimulationConfig());
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        WrathOfTheBerserker.GetBerserkDamageBonus(state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Not_Skilled_In_SupremeWrathOfTheBerserker()
    {
        var state = new SimulationState(new SimulationConfig());
        state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Player.Auras.Add(Aura.Berserking);

        WrathOfTheBerserker.GetBerserkDamageBonus(state).Should().Be(1.0);
    }
}
