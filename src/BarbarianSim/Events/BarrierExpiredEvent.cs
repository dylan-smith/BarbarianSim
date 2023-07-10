namespace BarbarianSim.Events
{
    public class BarrierExpiredEvent : EventInfo
    {
        public Barrier Barrier { get; init; }

        public BarrierExpiredEvent(double timestamp, Barrier barrier) : base(timestamp) => Barrier = barrier;

        public override void ProcessEvent(SimulationState state)
        {
            state.Player.Barriers.Remove(Barrier);
        }
    }
}
