using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class BleedCompletedEventHandler : EventHandler<BleedCompletedEvent>
{
    public BleedCompletedEventHandler(TotalDamageMultiplierCalculator totalDamageMultiplierCalculator) => _totalDamageMultiplierCalculator = totalDamageMultiplierCalculator;

    private readonly TotalDamageMultiplierCalculator _totalDamageMultiplierCalculator;

    public override void ProcessEvent(BleedCompletedEvent e, SimulationState state)
    {
        if (!state.Events.Any(x => x is BleedCompletedEvent))
        {
            e.Target.Auras.Remove(Aura.Bleeding);
        }

        var damageType = DamageType.Physical | DamageType.DamageOverTime;
        var damageMultiplier = _totalDamageMultiplierCalculator.Calculate(state, damageType, e.Target, SkillType.None, DamageSource.Bleeding, null);

        var damage = e.Damage * damageMultiplier;

        e.DamageEvent = new DamageEvent(e.Timestamp, damage, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, e.Target);
        state.Events.Add(e.DamageEvent);
    }
}
