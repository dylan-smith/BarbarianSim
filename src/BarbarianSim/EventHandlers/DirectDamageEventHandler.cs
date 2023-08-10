﻿using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class DirectDamageEventHandler : EventHandler<DirectDamageEvent>
{
    public DirectDamageEventHandler(TotalDamageMultiplierCalculator totalDamageMultiplierCalculator,
                                    CritChanceCalculator critChanceCalculator,
                                    CritDamageCalculator critDamageCalculator,
                                    OverpowerDamageCalculator overpowerDamageCalculator,
                                    LuckyHitChanceCalculator luckyHitChanceCalculator,
                                    RandomGenerator randomGenerator,
                                    SimLogger log)
    {
        _totalDamageMultiplierCalculator = totalDamageMultiplierCalculator;
        _critChanceCalculator = critChanceCalculator;
        _critDamageCalculator = critDamageCalculator;
        _overpowerDamageCalculator = overpowerDamageCalculator;
        _luckyHitChanceCalculator = luckyHitChanceCalculator;
        _randomGenerator = randomGenerator;
        _log = log;
    }

    private readonly TotalDamageMultiplierCalculator _totalDamageMultiplierCalculator;
    private readonly CritChanceCalculator _critChanceCalculator;
    private readonly CritDamageCalculator _critDamageCalculator;
    private readonly OverpowerDamageCalculator _overpowerDamageCalculator;
    private readonly LuckyHitChanceCalculator _luckyHitChanceCalculator;
    private readonly RandomGenerator _randomGenerator;
    private readonly SimLogger _log;

    public override void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        var damageMultiplier = _totalDamageMultiplierCalculator.Calculate(state, e.DamageType, e.Enemy, e.SkillType, e.DamageSource, e.Weapon);

        var damage = e.BaseDamage * damageMultiplier;
        _log.Verbose($"Damage: {e.BaseDamage:F2} * {damageMultiplier:F2} = {damage:F2}");

        var critChance = _critChanceCalculator.Calculate(state, e.DamageType, e.Enemy, e.Weapon);
        var critRoll = _randomGenerator.Roll(RollType.CriticalStrike);

        var damageType = e.DamageType | DamageType.Direct;

        if (critRoll <= critChance)
        {
            var expertise = e.Weapon?.Expertise ?? Expertise.NA;
            var critMultiplier = _critDamageCalculator.Calculate(state, expertise, e.Weapon, e.Enemy);
            damage *= critMultiplier;
            damageType |= DamageType.CriticalStrike;
            _log.Verbose($"Crit Multiplier = {critMultiplier}x");
        }

        var overpowerRoll = _randomGenerator.Roll(RollType.Overpower);

        if (overpowerRoll <= 0.03)
        {
            // https://gamerant.com/diablo-4-what-is-overpower-damage-guide/
            var overpowerMultiplier = _overpowerDamageCalculator.Calculate(state);
            var overpowerDamage = (state.Player.Life + state.Player.Fortify) * damageMultiplier * overpowerMultiplier;
            _log.Verbose($"Overpower Damage: ({state.Player.Life:F2} (Life) + {state.Player.Fortify:F2} (Fortify)) * {overpowerMultiplier:F2}x (Overpower Bonus) * {damageMultiplier:F2}x (Damage Bonuses)");

            damage += overpowerDamage;
            damageType |= DamageType.Overpower;
        }

        e.DamageEvent = new DamageEvent(e.Timestamp, e.Source, damage, damageType, e.DamageSource, e.SkillType, e.Enemy);
        state.Events.Add(e.DamageEvent);
        _log.Verbose($"Created DamageEvent for {damage:F2} damage on Enemy #{e.Enemy.Id} of type {damageType}");

        var luckyRoll = _randomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (e.LuckyHitChance + _luckyHitChanceCalculator.Calculate(state, e.Weapon)))
        {
            e.LuckyHitEvent = new LuckyHitEvent(e.Timestamp, e.Source, e.SkillType, e.Enemy, e.Weapon);
            state.Events.Add(e.LuckyHitEvent);
            _log.Verbose($"Created LuckyHitEvent");
        }
    }
}
