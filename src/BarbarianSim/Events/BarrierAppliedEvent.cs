using BarbarianSim.Enums;

namespace BarbarianSim.Events
{
    public class BarrierAppliedEvent : EventInfo
    {
        public int BarrierAmount { get; init; }
        public double Duration { get; init; }
        public Barrier Barrier { get; set; }
        public BarrierExpiredEvent BarrierExpiredEvent { get; set; }

        public BarrierAppliedEvent(double timestamp, int barrierAmount, double duration) : base(timestamp)
        {
            BarrierAmount = barrierAmount;
            Duration = duration;
        }

        public override void ProcessEvent(SimulationState state)
        {
            Barrier = new Barrier(BarrierAmount);

            state.Player.Barriers.Add(Barrier);
            state.Player.Auras.Add(Aura.Barrier);

            BarrierExpiredEvent = new BarrierExpiredEvent(Timestamp + Duration, Barrier);
            state.Events.Add(BarrierExpiredEvent);
        }
    }
}
