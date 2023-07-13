using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Skills;

namespace BarbarianSim.Events
{
    public class GutteralYellProcEvent : EventInfo
    {
        public GutteralYellProcEvent(AuraAppliedEventFactory auraAppliedEventFactory, double timestamp) : base(timestamp)
        {
            _auraAppliedEventFactory = auraAppliedEventFactory;
        }

        private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

        public AuraAppliedEvent GutteralYellAuraAppliedEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            GutteralYellAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, GutteralYell.DURATION, Aura.GutteralYell);
            state.Events.Add(GutteralYellAuraAppliedEvent);
        }
    }
}
