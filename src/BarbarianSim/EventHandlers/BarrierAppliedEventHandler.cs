using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class BarrierAppliedEventHandler : EventHandler<BarrierAppliedEvent>
{
    public BarrierAppliedEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(BarrierAppliedEvent e, SimulationState state)
    {
        e.Barrier = new Barrier(e.BarrierAmount);

        state.Player.Barriers.Add(e.Barrier);

        e.BarrierExpiredEvent = new BarrierExpiredEvent(e.Timestamp + e.Duration, e.Source, e.Barrier);
        state.Events.Add(e.BarrierExpiredEvent);
        _log.Verbose($"Created BarrierExpiredEvent for {e.Barrier:F2} barrier expiring in {e.Duration:F2} seconds");
    }
}
