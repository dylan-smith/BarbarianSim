using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class BleedAppliedEventHandler : EventHandler<BleedAppliedEvent>
{
    public BleedAppliedEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(BleedAppliedEvent e, SimulationState state)
    {
        e.Target.Auras.Add(Aura.Bleeding);
        e.BleedCompletedEvent = new BleedCompletedEvent(e.Timestamp + e.Duration, e.Source, e.Damage, e.Target);
        state.Events.Add(e.BleedCompletedEvent);
        _log.Verbose($"Created BleedCompletedEvent for {e.Damage:F2} damage in {e.Duration:F2} seconds for Enemy #{e.Target.Id}");
    }
}
