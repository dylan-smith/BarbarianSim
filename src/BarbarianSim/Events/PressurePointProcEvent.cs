namespace BarbarianSim.Events
{
    public class PressurePointProcEvent : EventInfo
    {
        public PressurePointProcEvent(double timestamp, EnemyState target) : base(timestamp) => Target = target;

        private const double VULNERABLE_DURATION = 2.0;

        public EnemyState Target { get; init; }
        public VulnerableAppliedEvent VulnerableAppliedEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            VulnerableAppliedEvent = new VulnerableAppliedEvent(Timestamp, Target, VULNERABLE_DURATION);

            state.Events.Add(VulnerableAppliedEvent);
        }
    }
}
