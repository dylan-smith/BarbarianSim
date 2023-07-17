using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class AspectOfTheProtectorProcEventHandler : EventHandler<AspectOfTheProtectorProcEvent>
{
    public override void ProcessEvent(AspectOfTheProtectorProcEvent e, SimulationState state)
    {
        e.AspectOfTheProtectorCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, AspectOfTheProtector.COOLDOWN, Aura.AspectOfTheProtectorCooldown);
        state.Events.Add(e.AspectOfTheProtectorCooldownAuraAppliedEvent);

        e.BarrierAppliedEvent = new BarrierAppliedEvent(e.Timestamp, e.BarrierAmount, AspectOfTheProtector.BARRIER_EXPIRY);
        state.Events.Add(e.BarrierAppliedEvent);
    }
}
