using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events
{
    public class GutteralYellProcEvent : EventInfo
    {
        public GutteralYellProcEvent(double timestamp) : base(timestamp)
        {
        }

        public AuraAppliedEvent GutteralYellAuraAppliedEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            GutteralYellAuraAppliedEvent = new AuraAppliedEvent(Timestamp, GutteralYell.DURATION, Aura.GutteralYell);
            state.Events.Add(GutteralYellAuraAppliedEvent);
        }
    }
}
