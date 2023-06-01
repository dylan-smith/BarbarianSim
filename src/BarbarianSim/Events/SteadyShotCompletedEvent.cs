namespace HunterSim.Events
{
    public class SteadyShotCompletedEvent : EventInfo
    {
        public CastCompletedEvent CastCompletedEvent { get; private set; }
        public DamageEvent DamageEvent { get; private set; }

        public SteadyShotCompletedEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            
        }
    }
}
