using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public static class WrathOfTheBerserker
{
    public const double BERSERKING_DURATION = 5;
    public const double UNSTOPPABLE_DURATION = 5;
    public const double DURATION = 10;
    public const double COOLDOWN = 60;
    public const double MOVEMENT_SPEED_FROM_PRIME = 20;
    public const double RESOURCE_GENERATION_FROM_PRIME = 1.3;

    // Explode into rage, Knocking Back surrounding enemies and gaining Berserking and Unstoppable for 5 seconds. For the next 10 seconds dealing Direct Damage with Basic Skills grants Berserking for 5 seconds. (Cooldown: 60 seconds)
    // Prime: While Wrath of the Berserker is active, gain 20%[+] increased Movement Speed and increase Fury Generation by 30%[x]
    // Supreme: While Wrath of the Berserker is active, every 50 Fury you spend increases Berserk's damage bonus by 25%[x]
    public static bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.WrathOfTheBerserkerCooldown);

    public static void Use(SimulationState state) => state.Events.Add(new WrathOfTheBerserkerEvent(state.CurrentTime));

    public static void ProcessEvent(DamageEvent damageEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.WrathOfTheBerserker) &&
            state.Player.Auras.Contains(Aura.WrathOfTheBerserker) &&
            damageEvent.DamageType != DamageType.DamageOverTime &&
            damageEvent.SkillType == SkillType.Basic)
        {
            state.Events.Add(new BerserkingAppliedEvent(damageEvent.Timestamp, BERSERKING_DURATION));
        }
    }

    public static double GetBerserkDamageBonus(SimulationState state)
    {
        if (state.Player.Auras.Contains(Aura.WrathOfTheBerserker) && state.Player.Auras.Contains(Aura.Berserking) && state.Config.Skills.ContainsKey(Skill.SupremeWrathOfTheBerserker))
        {
            var startTime = state.ProcessedEvents.OrderBy(e => e.Timestamp).Last(e => e is WrathOfTheBerserkerEvent).Timestamp;
            var totalFurySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Where(e => e.Timestamp >= startTime).Sum(e => e.FurySpent);

            return Math.Pow(1.25, Math.Floor(totalFurySpent / 50));
        }

        return 1.0;
    }
}
