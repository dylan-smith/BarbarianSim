using BarbarianSim.Enums;

namespace BarbarianSim.Events
{
    public class AspectOfTheProtectorProcEvent : EventInfo
    {
        public AspectOfTheProtectorProcEvent(double timestamp, int barrierAmount) : base(timestamp) => BarrierAmount = barrierAmount;

        private const double BARRIER_EXPIRY = 10.0;

        public int BarrierAmount { get; init; }
        public BarrierAppliedEvent BarrierAppliedEvent { get; set; }
        public CooldownCompletedEvent AspectOfTheProtectorCooldownCompletedEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            BarrierAppliedEvent = new BarrierAppliedEvent(Timestamp, BarrierAmount, BARRIER_EXPIRY);
            AspectOfTheProtectorCooldownCompletedEvent = new CooldownCompletedEvent(Timestamp + 30, Aura.AspectOfTheProtectorCooldown);

            state.Player.Auras.Add(Aura.AspectOfTheProtectorCooldown);
            state.Events.Add(BarrierAppliedEvent);
            state.Events.Add(AspectOfTheProtectorCooldownCompletedEvent);
        }

        public override string ToString() => $"[{Timestamp:F1}] Aspect of the Protector proc for {BarrierAmount} barrier";
    }
}
