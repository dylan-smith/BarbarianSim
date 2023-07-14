﻿using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public class RallyingCry
{
    public const double MOVEMENT_SPEED = 30.0;
    public const double DURATION = 6.0;
    public const double COOLDOWN = 25.0;
    public const double FURY_FROM_TACTICAL_RALLYING_CRY = 20.0;
    public const double RESOURCE_GENERATION_FROM_TACTICAL_RALLYING_CRY = 1.20;
    public const double FORTIFY_FROM_STRATEGIC_RALLYING_CRY = 0.1;
    public const double DIRECT_DAMAGE_FORTIFY_FROM_STRATEGIC_RALLYING_CRY = 0.02;

    // Bellow a Rallying Cry, increasing your Movement Speed by 30%[+] and Resource Generation by 40%[x] for 6.0 seconds, and Nearby allies for 3.0 seconds (Cooldown: 25 seconds)
    public bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.RallyingCryCooldown);

    public void Use(SimulationState state) => state.Events.Add(new RallyingCryEvent(state.CurrentTime));

    public double GetResourceGeneration(SimulationState state)
    {
        var skillPoints = state.Config.Gear.AllGear.Sum(g => g.RallyingCry);

        if (state.Config.Skills.TryGetValue(Skill.RallyingCry, out var pointsSpent))
        {
            skillPoints += pointsSpent;
        }

        return skillPoints switch
        {
            1 => 1.40,
            2 => 1.44,
            3 => 1.48,
            4 => 1.52,
            >= 5 => 1.56,
            _ => 1.0,
        };
    }

    public void ProcessEvent(DirectDamageEvent damageEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry) &&
            state.Player.Auras.Contains(Aura.RallyingCry))
        {
            state.Events.Add(new FortifyGeneratedEvent(damageEvent.Timestamp, DIRECT_DAMAGE_FORTIFY_FROM_STRATEGIC_RALLYING_CRY * state.Player.BaseLife));
        }
    }
}
