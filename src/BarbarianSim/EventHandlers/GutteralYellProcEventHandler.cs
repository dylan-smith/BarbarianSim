using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class GutteralYellProcEventHandler : EventHandler<GutteralYellProcEvent>
{
    public override void ProcessEvent(GutteralYellProcEvent e, SimulationState state)
    {
        e.GutteralYellAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, GutteralYell.DURATION, Aura.GutteralYell);
        state.Events.Add(e.GutteralYellAuraAppliedEvent);
    }
}
