using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class BleedAppliedEventHandler : EventHandler<BleedAppliedEvent>
{
    public override void ProcessEvent(BleedAppliedEvent e, SimulationState state)
    {
        e.Target.Auras.Add(Aura.Bleeding);
        e.BleedCompletedEvent = new BleedCompletedEvent(e.Timestamp + e.Duration, e.Source, e.Damage, e.Target);
        state.Events.Add(e.BleedCompletedEvent);
    }
}
