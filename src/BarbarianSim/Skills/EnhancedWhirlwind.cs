using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public static class EnhancedWhirlwind
{
    // Gain 1 Fury each time Whirlwind deals damage to an enemy, 4 Fury against Elite enemies
    public const double FURY_GAINED = 1;
    public const double ELITE_FURY_GAINED = 4;

    public static void ProcessEvent(DamageEvent damageEvent, SimulationState state)
    {
        if (damageEvent.DamageSource == DamageSource.Whirlwind && state.Config.Skills.ContainsKey(Skill.EnhancedWhirlwind))
        {
            var furyGenerated = state.Config.EnemySettings.IsElite ? ELITE_FURY_GAINED : FURY_GAINED;

            var furyGeneratedEvent = new FuryGeneratedEvent(damageEvent.Timestamp, furyGenerated);
            state.Events.Add(furyGeneratedEvent);
        }
    }
}
