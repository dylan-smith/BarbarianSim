using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class DirectDamageEventHandler : EventHandler<DirectDamageEvent>
{
    public override void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, e.DamageType, e.Enemy, e.SkillType, e.DamageSource);

        var damage = e.BaseDamage * damageMultiplier;

        var critChance = CritChanceCalculator.Calculate(state, e.DamageType);
        var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

        var damageType = e.DamageType | DamageType.Direct;

        if (critRoll <= critChance)
        {
            damage *= CritDamageCalculator.Calculate(state, e.Expertise);
            damageType |= DamageType.CriticalStrike;
        }

        e.DamageEvent = new DamageEvent(e.Timestamp, damage, damageType, e.DamageSource, e.SkillType, e.Enemy);
        state.Events.Add(e.DamageEvent);

        var luckyRoll = RandomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (e.LuckyHitChance + LuckyHitChanceCalculator.Calculate(state)))
        {
            e.LuckyHitEvent = new LuckyHitEvent(e.Timestamp, e.SkillType, e.Enemy);
            state.Events.Add(e.LuckyHitEvent);
        }
    }
}
