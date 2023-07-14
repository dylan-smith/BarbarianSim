﻿using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class EnhancedLungingStrike
{
    // Lunging strike deals 30%[x] increased damage and Heals you for 2% Maximum Life when it damages a Healthy enemy
    public const double DAMAGE_MULTIPLIER = 1.3;
    public const double HEAL_PERCENT = 0.02;

    public EnhancedLungingStrike(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public void ProcessEvent(LungingStrikeEvent lungingStrikeEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.EnhancedLungingStrike) &&
            lungingStrikeEvent.Target.IsHealthy())
        {
            var healingEvent = new HealingEvent(lungingStrikeEvent.Timestamp, _maxLifeCalculator.Calculate(state) * HEAL_PERCENT);
            state.Events.Add(healingEvent);
        }
    }
}
