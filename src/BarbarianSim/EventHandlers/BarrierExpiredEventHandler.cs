using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class BarrierExpiredEventHandler : EventHandler<BarrierExpiredEvent>
{
    public override void ProcessEvent(BarrierExpiredEvent e, SimulationState state) => state.Player.Barriers.Remove(e.Barrier);
}
