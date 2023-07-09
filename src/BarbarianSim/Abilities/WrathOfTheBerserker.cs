using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public static class WrathOfTheBerserker
{
    public const double BERSERKING_DURATION = 5;
    public const double UNSTOPPABLE_DURATION = 5;
    public const double DURATION = 10;
    public const double COOLDOWN = 60;

    // Explode into rage, Knocking Back surrounding enemies and gaining Berserking and Unstoppable for 5 seconds. For the next 10 seconds dealing Direct Damage with Basic Skills grants Berserking for 5 seconds. (Cooldown: 60 seconds)
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
}
