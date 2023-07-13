﻿using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Aspects;

public class EdgemastersAspect : Aspect
{
    // Skills deal up to 10-20%[x] increased damage based on your available Primary Resource when cast, receiving the maximum benefit while you have full Primary Resource
    public int Damage { get; init; }

    public EdgemastersAspect(MaxFuryCalculator maxFuryCalculator, int damage)
    {
        _maxFuryCalculator = maxFuryCalculator;
        Damage = damage;
    }

    private readonly MaxFuryCalculator _maxFuryCalculator;

    public double GetDamageBonus(SimulationState state, SkillType skillType)
    {
        if (skillType != SkillType.None)
        {
            var maxFury = _maxFuryCalculator.Calculate(state);
            var furyMultiplier = state.Player.Fury / maxFury;

            return 1 + (Damage / 100.0 * furyMultiplier);
        }

        return 1.0;
    }
}
