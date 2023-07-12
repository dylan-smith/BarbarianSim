using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public static class EnhancedLungingStrike
{
    // Lunging strike deals 30%[x] increased damage and Heals you for 2% Maximum Life when it damages a Healthy enemy
    public const double DAMAGE_MULTIPLIER = 1.3;
    public const double HEAL_PERCENT = 0.02;

    public static void ProcessEvent(DamageEvent damageEvent, SimulationState state)
    {
        if (damageEvent.DamageSource == DamageSource.LungingStrike &&
            state.Config.Skills.ContainsKey(Skill.EnhancedLungingStrike) &&
            damageEvent.Target.IsHealthy())
        {
            var healingEvent = new HealingEvent(damageEvent.Timestamp, MaxLifeCalculator.Calculate(state) * HEAL_PERCENT);
            state.Events.Add(healingEvent);
        }
    }
}
