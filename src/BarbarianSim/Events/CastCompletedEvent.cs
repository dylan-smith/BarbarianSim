namespace HunterSim.Events
{
    public class CastCompletedEvent : EventInfo
    {
        public CastCompletedEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            state.Auras.Remove(Aura.CastInProgress);
        }
    }
}
