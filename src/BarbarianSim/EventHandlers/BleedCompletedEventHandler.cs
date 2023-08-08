using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class BleedCompletedEventHandler : EventHandler<BleedCompletedEvent>
{
    public BleedCompletedEventHandler(TotalDamageMultiplierCalculator totalDamageMultiplierCalculator, SimLogger log)
    {
        _totalDamageMultiplierCalculator = totalDamageMultiplierCalculator;
        _log = log;
    }

    private readonly TotalDamageMultiplierCalculator _totalDamageMultiplierCalculator;
    private readonly SimLogger _log;

    public override void ProcessEvent(BleedCompletedEvent e, SimulationState state)
    {
        if (!state.Events.Any(x => x is BleedCompletedEvent))
        {
            e.Target.Auras.Remove(Aura.Bleeding);
        }

        var damageType = DamageType.Physical | DamageType.DamageOverTime;
        var damageMultiplier = _totalDamageMultiplierCalculator.Calculate(state, damageType, e.Target, SkillType.None, DamageSource.Bleeding, null);

        var damage = e.Damage * damageMultiplier;
        _log.Verbose($"Total Bleeding damage: {e.Damage:F2} * {damageMultiplier:F2} = {damage:F2}");

        e.DamageEvent = new DamageEvent(e.Timestamp, e.Source, damage, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, e.Target);
        state.Events.Add(e.DamageEvent);
        _log.Verbose($"Created DamageEvent for {damage:F2} damage on Enemy #{e.Target.Id}");
    }
}
