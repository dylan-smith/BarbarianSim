namespace BarbarianSim.Events
{
    public class AspectOfTheProtectorProcEvent : EventInfo
    {
        public AspectOfTheProtectorProcEvent(double timestamp, int barrierAmount) : base(timestamp) => BarrierAmount = barrierAmount;

        public int BarrierAmount { get; init; }
        public BarrierAppliedEvent BarrierAppliedEvent { get; set; }
        public AuraAppliedEvent AspectOfTheProtectorCooldownAuraAppliedEvent { get; set; }

        public override string ToString() => $"[{Timestamp:F1}] Aspect of the Protector proc for {BarrierAmount} barrier";
    }
}
