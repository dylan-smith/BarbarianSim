using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class BleedCompletedEventHandler : EventHandler<BleedCompletedEvent>
{
    public override void ProcessEvent(BleedCompletedEvent e, SimulationState state)
    {
        if (!state.Events.Any(x => x is BleedCompletedEvent))
        {
            e.Target.Auras.Remove(Aura.Bleeding);
        }

        e.DamageEvent = new DamageEvent(e.Timestamp, e.Damage, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, e.Target);
        state.Events.Add(e.DamageEvent);
    }
}
