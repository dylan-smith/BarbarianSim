using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class PressurePointProcEventHandler : EventHandler<PressurePointProcEvent>
{
    public override void ProcessEvent(PressurePointProcEvent e, SimulationState state)
    {
        e.VulnerableAppliedEvent = new VulnerableAppliedEvent(e.Timestamp, e.Target, PressurePoint.VULNERABLE_DURATION);

        state.Events.Add(e.VulnerableAppliedEvent);
    }
}
