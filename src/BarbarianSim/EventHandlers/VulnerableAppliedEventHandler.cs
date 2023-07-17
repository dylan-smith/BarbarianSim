using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class VulnerableAppliedEventHandler : EventHandler<VulnerableAppliedEvent>
{
    public override void ProcessEvent(VulnerableAppliedEvent e, SimulationState state)
    {
        e.Target.Auras.Add(Aura.Vulnerable);

        e.VulnerableExpiredEvent = new AuraExpiredEvent(e.Timestamp + e.Duration, e.Target, Aura.Vulnerable);
        state.Events.Add(e.VulnerableExpiredEvent);
    }
}
