using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events
{
    public class AspectOfTheProtectorProcEvent : EventInfo
    {
        public AspectOfTheProtectorProcEvent(AuraAppliedEventFactory auraAppliedEventFactory,
                                             BarrierAppliedEventFactory barrierAppliedEventFactory,
                                             double timestamp,
                                             int barrierAmount) : base(timestamp)
        {
            _auraAppliedEventFactory = auraAppliedEventFactory;
            _barrierAppliedEventFactory = barrierAppliedEventFactory;
            BarrierAmount = barrierAmount;
        }

        private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
        private readonly BarrierAppliedEventFactory _barrierAppliedEventFactory;

        private const double BARRIER_EXPIRY = 10.0;

        public int BarrierAmount { get; init; }
        public BarrierAppliedEvent BarrierAppliedEvent { get; set; }
        public AuraAppliedEvent AspectOfTheProtectorCooldownAuraAppliedEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            AspectOfTheProtectorCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, 30, Aura.AspectOfTheProtectorCooldown);
            state.Events.Add(AspectOfTheProtectorCooldownAuraAppliedEvent);

            BarrierAppliedEvent = _barrierAppliedEventFactory.Create(Timestamp, BarrierAmount, BARRIER_EXPIRY);
            state.Events.Add(BarrierAppliedEvent);
        }

        public override string ToString() => $"[{Timestamp:F1}] Aspect of the Protector proc for {BarrierAmount} barrier";
    }
}
