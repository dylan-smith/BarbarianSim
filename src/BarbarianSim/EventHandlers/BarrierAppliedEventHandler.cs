using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class BarrierAppliedEventHandler : EventHandler<BarrierAppliedEvent>
{
    public override void ProcessEvent(BarrierAppliedEvent e, SimulationState state)
    {
        e.Barrier = new Barrier(e.BarrierAmount);

        e.BarrierAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.Barrier);
        state.Events.Add(e.BarrierAuraAppliedEvent);

        state.Player.Barriers.Add(e.Barrier);

        e.BarrierExpiredEvent = new BarrierExpiredEvent(e.Timestamp + e.Duration, e.Barrier);
        state.Events.Add(e.BarrierExpiredEvent);
    }
}
