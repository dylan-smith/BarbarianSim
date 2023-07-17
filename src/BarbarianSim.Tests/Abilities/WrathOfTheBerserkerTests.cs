﻿using BarbarianSim.Abilities;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.Abilities;

public class WrathOfTheBerserkerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly WrathOfTheBerserker _wrathOfTheBerserker = new();

    [Fact]
    public void CanUse_Returns_True_If_Not_On_Cooldown()
    {
        _wrathOfTheBerserker.CanUse(_state).Should().BeTrue();
    }

    [Fact]
    public void CanUse_Returns_False_If_On_Cooldown()
    {
        _state.Player.Auras.Add(Aura.WrathOfTheBerserkerCooldown);

        _wrathOfTheBerserker.CanUse(_state).Should().BeFalse();
    }

    [Fact]
    public void Use_Creates_WrathOfTheBerserkerEvent()
    {
        _state.CurrentTime = 123;

        _wrathOfTheBerserker.Use(_state);

        _state.Events.Should().ContainSingle(e => e is WrathOfTheBerserkerEvent);
        _state.Events.OfType<WrathOfTheBerserkerEvent>().First().Timestamp.Should().Be(123);
    }

    [Fact]
    public void Creates_BerserkingAppliedEvent_On_Direct_Damage_With_Basic_Skills()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct | DamageType.CriticalStrike, DamageSource.LungingStrike, SkillType.Basic, 0, Expertise.Polearm, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().ContainSingle(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
        _state.Events.OfType<AuraAppliedEvent>().First().Timestamp.Should().Be(123);
        _state.Events.OfType<AuraAppliedEvent>().First().Duration.Should().Be(5);
    }

    [Fact]
    public void Does_Not_Berserk_If_Missing_Skill()
    {
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, 0, Expertise.Polearm, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void Does_Not_Berserk_If_Core_Skill()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Core, 0, Expertise.Polearm, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void Does_Not_Berserk_If_Aura_Missing()
    {
        _state.Config.Skills.Add(Skill.WrathOfTheBerserker, 1);
        var directDamageEvent = new DirectDamageEvent(123, 500, DamageType.Direct, DamageSource.LungingStrike, SkillType.Basic, 0, Expertise.Polearm, _state.Enemies.First());

        _wrathOfTheBerserker.ProcessEvent(directDamageEvent, _state);

        _state.Events.Should().NotContain(e => e is AuraAppliedEvent && ((AuraAppliedEvent)e).Aura == Aura.Berserking);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_3x_When_157_Fury_Spent()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _wrathOfTheBerserker.GetBerserkDamageBonus(_state).Should().Be(1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Sums_All_FurySpentEvents()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(123.5, 46, SkillType.None) { FurySpent = 46 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _wrathOfTheBerserker.GetBerserkDamageBonus(_state).Should().Be(1.25 * 1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Excludes_Fury_Spent_Before_Wrath()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(122, 157, SkillType.None) { FurySpent = 157 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _wrathOfTheBerserker.GetBerserkDamageBonus(_state).Should().Be(1.25 * 1.25 * 1.25);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_FurySpent_Less_Than_50()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(123.5, 12, SkillType.None) { FurySpent = 12 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, 25, SkillType.None) { FurySpent = 25 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _wrathOfTheBerserker.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Wrath_Not_Active()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.Berserking);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _wrathOfTheBerserker.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Not_Berserking()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Config.Skills.Add(Skill.SupremeWrathOfTheBerserker, 1);

        _wrathOfTheBerserker.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }

    [Fact]
    public void GetBerserkDamageBonus_Returns_1_When_Not_Skilled_In_SupremeWrathOfTheBerserker()
    {
        _state.ProcessedEvents.Add(new WrathOfTheBerserkerEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(127, 157, SkillType.None) { FurySpent = 157 });
        _state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        _state.Player.Auras.Add(Aura.Berserking);

        _wrathOfTheBerserker.GetBerserkDamageBonus(_state).Should().Be(1.0);
    }
}
