using BarbarianSim.Enums;

namespace BarbarianSim.Events
{
    public class BarrierAppliedEvent : EventInfo
    {
        public double BarrierAmount { get; init; }
        public double Duration { get; init; }
        public Barrier Barrier { get; set; }
        public BarrierExpiredEvent BarrierExpiredEvent { get; set; }
        public AuraAppliedEvent BarrierAuraAppliedEvent { get; set; }

        public BarrierAppliedEvent(double timestamp, double barrierAmount, double duration) : base(timestamp)
        {
            BarrierAmount = barrierAmount;
            Duration = duration;
        }

        public override void ProcessEvent(SimulationState state)
        {
            Barrier = new Barrier(BarrierAmount);

            BarrierAuraAppliedEvent = new AuraAppliedEvent(Timestamp, Duration, Aura.Barrier);
            state.Events.Add(BarrierAuraAppliedEvent);

            state.Player.Barriers.Add(Barrier);

            BarrierExpiredEvent = new BarrierExpiredEvent(Timestamp + Duration, Barrier);
            state.Events.Add(BarrierExpiredEvent);
        }

        public override string ToString() => $"{base.ToString()} - {BarrierAmount} barrier applied for {Duration} seconds";
    }
}
