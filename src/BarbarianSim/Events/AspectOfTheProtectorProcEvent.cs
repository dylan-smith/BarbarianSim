using BarbarianSim.Enums;

namespace BarbarianSim.Events
{
    public class AspectOfTheProtectorProcEvent : EventInfo
    {
        public AspectOfTheProtectorProcEvent(double timestamp, int barrierAmount) : base(timestamp) => BarrierAmount = barrierAmount;

        private const double BARRIER_EXPIRY = 10.0;

        public int BarrierAmount { get; init; }
        public BarrierAppliedEvent BarrierAppliedEvent { get; set; }
        public AuraAppliedEvent AspectOfTheProtectorCooldownAuraAppliedEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            AspectOfTheProtectorCooldownAuraAppliedEvent = new AuraAppliedEvent(Timestamp, 30, Aura.AspectOfTheProtectorCooldown);
            state.Events.Add(AspectOfTheProtectorCooldownAuraAppliedEvent);

            BarrierAppliedEvent = new BarrierAppliedEvent(Timestamp, BarrierAmount, BARRIER_EXPIRY);
            state.Events.Add(BarrierAppliedEvent);
        }

        public override string ToString() => $"[{Timestamp:F1}] Aspect of the Protector proc for {BarrierAmount} barrier";
    }
}
