using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public class WrathOfTheBerserker : IHandlesEvent<DirectDamageEvent>
{
    public const double BERSERKING_DURATION = 5;
    public const double UNSTOPPABLE_DURATION = 5;
    public const double DURATION = 10;
    public const double COOLDOWN = 60;

    // Explode into rage, Knocking Back surrounding enemies and gaining Berserking and Unstoppable for 5 seconds. For the next 10 seconds dealing Direct Damage with Basic Skills grants Berserking for 5 seconds. (Cooldown: 60 seconds)
    // Prime: While Wrath of the Berserker is active, gain 20%[+] increased Movement Speed and increase Fury Generation by 30%[x]
    // Supreme: While Wrath of the Berserker is active, every 50 Fury you spend increases Berserk's damage bonus by 25%[x]
    public virtual bool CanUse(SimulationState state) =>
        state.Config.Skills.TryGetValue(Skill.WrathOfTheBerserker, out var skillPoints)
        && skillPoints > 0
        && !state.Player.Auras.Contains(Aura.WrathOfTheBerserkerCooldown);

    public virtual void Use(SimulationState state) => state.Events.Add(new WrathOfTheBerserkerEvent(state.CurrentTime));

    public void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.WrathOfTheBerserker) &&
            state.Player.Auras.Contains(Aura.WrathOfTheBerserker) &&
            e.SkillType == SkillType.Basic)
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, BERSERKING_DURATION, Aura.Berserking));
        }
    }
}
