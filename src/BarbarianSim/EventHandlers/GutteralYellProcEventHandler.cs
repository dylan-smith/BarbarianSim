using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class GutteralYellProcEventHandler : EventHandler<GutteralYellProcEvent>
{
    public GutteralYellProcEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(GutteralYellProcEvent e, SimulationState state)
    {
        e.GutteralYellAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Gutteral Yell", GutteralYell.DURATION, Aura.GutteralYell);
        state.Events.Add(e.GutteralYellAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Gutteral Yell for {GutteralYell.DURATION} seconds");
    }
}
