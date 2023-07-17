using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class PressurePointProcEventHandler : EventHandler<PressurePointProcEvent>
{
    public override void ProcessEvent(PressurePointProcEvent e, SimulationState state)
    {
        e.VulnerableAppliedEvent = new AuraAppliedEvent(e.Timestamp, PressurePoint.VULNERABLE_DURATION, Aura.Vulnerable, e.Target);

        state.Events.Add(e.VulnerableAppliedEvent);
    }
}
