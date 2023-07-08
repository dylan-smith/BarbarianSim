using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events
{
    public class GutteralYellProcEvent : EventInfo
    {
        public GutteralYellProcEvent(double timestamp) : base(timestamp)
        {
        }

        public AuraExpiredEvent GutteralYellExpiredEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            state.Player.Auras.Add(Aura.GutteralYell);

            GutteralYellExpiredEvent = new AuraExpiredEvent(Timestamp + GutteralYell.DURATION, Aura.GutteralYell);
            state.Events.Add(GutteralYellExpiredEvent);
        }
    }
}
