using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events
{
    public class BarrierAppliedEvent : EventInfo
    {
        public double BarrierAmount { get; init; }
        public double Duration { get; init; }
        public Barrier Barrier { get; set; }
        public BarrierExpiredEvent BarrierExpiredEvent { get; set; }
        public AuraAppliedEvent BarrierAuraAppliedEvent { get; set; }

        public BarrierAppliedEvent(AuraAppliedEventFactory auraAppliedEventFactory,
                                   BarrierExpiredEventFactory barrierExpiredEventFactory,
                                   double timestamp,
                                   double barrierAmount,
                                   double duration) : base(timestamp)
        {
            _auraAppliedEventFactory = auraAppliedEventFactory;
            _barrierExpiredEventFactory = barrierExpiredEventFactory;
            BarrierAmount = barrierAmount;
            Duration = duration;
        }

        private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
        private readonly BarrierExpiredEventFactory _barrierExpiredEventFactory;

        public override void ProcessEvent(SimulationState state)
        {
            Barrier = new Barrier(BarrierAmount);

            BarrierAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, Duration, Aura.Barrier);
            state.Events.Add(BarrierAuraAppliedEvent);

            state.Player.Barriers.Add(Barrier);

            BarrierExpiredEvent = _barrierExpiredEventFactory.Create(Timestamp + Duration, Barrier);
            state.Events.Add(BarrierExpiredEvent);
        }

        public override string ToString() => $"{base.ToString()} - {BarrierAmount} barrier applied for {Duration} seconds";
    }
}
