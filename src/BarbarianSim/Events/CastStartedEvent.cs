namespace HunterSim.Events
{
    public class CastStartedEvent : EventInfo
    {
        public CastStartedEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            state.Auras.Add(Aura.CastInProgress);
        }
    }
}
