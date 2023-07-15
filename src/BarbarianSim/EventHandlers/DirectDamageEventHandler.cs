﻿using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class DirectDamageEventHandler : EventHandler<DirectDamageEvent>
{
    public DirectDamageEventHandler(TotalDamageMultiplierCalculator totalDamageMultiplierCalculator,
                                    CritChanceCalculator critChanceCalculator,
                                    CritDamageCalculator critDamageCalculator,
                                    LuckyHitChanceCalculator luckyHitChanceCalculator,
                                    RandomGenerator randomGenerator)
    {
        _totalDamageMultiplierCalculator = totalDamageMultiplierCalculator;
        _critChanceCalculator = critChanceCalculator;
        _critDamageCalculator = critDamageCalculator;
        _luckyHitChanceCalculator = luckyHitChanceCalculator;
        _randomGenerator = randomGenerator;
    }

    private readonly TotalDamageMultiplierCalculator _totalDamageMultiplierCalculator;
    private readonly CritChanceCalculator _critChanceCalculator;
    private readonly CritDamageCalculator _critDamageCalculator;
    private readonly LuckyHitChanceCalculator _luckyHitChanceCalculator;
    private readonly RandomGenerator _randomGenerator;

    public override void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        var damageMultiplier = _totalDamageMultiplierCalculator.Calculate(state, e.DamageType, e.Enemy, e.SkillType, e.DamageSource);

        var damage = e.BaseDamage * damageMultiplier;

        var critChance = _critChanceCalculator.Calculate(state, e.DamageType);
        var critRoll = _randomGenerator.Roll(RollType.CriticalStrike);

        var damageType = e.DamageType | DamageType.Direct;

        if (critRoll <= critChance)
        {
            damage *= _critDamageCalculator.Calculate(state, e.Expertise);
            damageType |= DamageType.CriticalStrike;
        }

        e.DamageEvent = new DamageEvent(e.Timestamp, damage, damageType, e.DamageSource, e.SkillType, e.Enemy);
        state.Events.Add(e.DamageEvent);

        var luckyRoll = _randomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (e.LuckyHitChance + _luckyHitChanceCalculator.Calculate(state)))
        {
            e.LuckyHitEvent = new LuckyHitEvent(e.Timestamp, e.SkillType, e.Enemy);
            state.Events.Add(e.LuckyHitEvent);
        }
    }
}
