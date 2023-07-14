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

        public override string ToString() => $"[{Timestamp:F1}] Aspect of the Protector proc for {BarrierAmount} barrier";
    }
}
