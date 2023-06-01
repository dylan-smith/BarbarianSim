namespace HunterSim.Events
{
    public class SteadyShotCastEvent : EventInfo
    {
        public CastStartedEvent CastStartedEvent { get; private set; }

        public SteadyShotCastEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            state.Auras.Add(Aura.GlobalCooldown);

            // TODO: Mana
            // TODO: Haste
            state.Events.Add(new SteadyShotCompletedEvent(Timestamp + 1.0));
            state.Events.Add(new GlobalCooldownExpiredEvent(Timestamp + 1.5));

            CastStartedEvent = new CastStartedEvent(Timestamp);
            state.Events.Add(CastStartedEvent);
        }
    }
}
