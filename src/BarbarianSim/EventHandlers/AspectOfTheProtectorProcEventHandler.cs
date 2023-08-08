using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class AspectOfTheProtectorProcEventHandler : EventHandler<AspectOfTheProtectorProcEvent>
{
    public AspectOfTheProtectorProcEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(AspectOfTheProtectorProcEvent e, SimulationState state)
    {
        e.AspectOfTheProtectorCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Aspect of the Protector", AspectOfTheProtector.COOLDOWN, Aura.AspectOfTheProtectorCooldown);
        state.Events.Add(e.AspectOfTheProtectorCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Aspect of the Protector cooldown for {AspectOfTheProtector.COOLDOWN} seconds");

        e.BarrierAppliedEvent = new BarrierAppliedEvent(e.Timestamp, "Aspect of the Protector", e.BarrierAmount, AspectOfTheProtector.BARRIER_EXPIRY);
        state.Events.Add(e.BarrierAppliedEvent);
        _log.Verbose($"Created BarrierAppliedEvent for {e.BarrierAmount:F2} barrier expiring in {AspectOfTheProtector.BARRIER_EXPIRY} seconds");
    }
}
