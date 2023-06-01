namespace HunterSim.Events
{
    public class GlobalCooldownExpiredEvent : EventInfo
    {
        public GlobalCooldownExpiredEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            
        }
    }
}
