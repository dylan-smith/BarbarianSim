using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class BarrierAppliedEventHandler : EventHandler<BarrierAppliedEvent>
{
    public override void ProcessEvent(BarrierAppliedEvent e, SimulationState state)
    {
        e.Barrier = new Barrier(e.BarrierAmount);

        state.Player.Barriers.Add(e.Barrier);

        e.BarrierExpiredEvent = new BarrierExpiredEvent(e.Timestamp + e.Duration, e.Source, e.Barrier);
        state.Events.Add(e.BarrierExpiredEvent);
    }
}
