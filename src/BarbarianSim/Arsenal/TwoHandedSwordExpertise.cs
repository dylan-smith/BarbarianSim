﻿using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Arsenal;

public class TwoHandedSwordExpertise : IHandlesEvent<DirectDamageEvent>
{
    // 20%[+] of Direct Damage you deal is inflicted as Bleeding damage over 5 seconds
    // Rank 10 Bonus: You deal 30%[x] increased bleeding damage for 5 seconds after killing an enemy
    public const double BLEED_DAMAGE = 0.2;
    public const double BLEED_DURATION = 5.0;

    public void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        if (e.Weapon?.Expertise == Expertise.TwoHandedSword || state.Config.PlayerSettings.ExpertiseTechnique == Expertise.TwoHandedSword)
        {
            var bleedDamage = e.BaseDamage * BLEED_DAMAGE;
            state.Events.Add(new BleedAppliedEvent(e.Timestamp, bleedDamage, BLEED_DURATION, e.Enemy));
        }
    }
}