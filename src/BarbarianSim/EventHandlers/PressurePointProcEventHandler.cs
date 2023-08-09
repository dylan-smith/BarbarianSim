using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class PressurePointProcEventHandler : EventHandler<PressurePointProcEvent>
{
    public PressurePointProcEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(PressurePointProcEvent e, SimulationState state)
    {
        e.VulnerableAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Pressure Point", PressurePoint.VULNERABLE_DURATION, Aura.Vulnerable, e.Target);
        state.Events.Add(e.VulnerableAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Vulnerable on Enemy #{e.Target.Id} for {PressurePoint.VULNERABLE_DURATION:F2} seconds");
    }
}
