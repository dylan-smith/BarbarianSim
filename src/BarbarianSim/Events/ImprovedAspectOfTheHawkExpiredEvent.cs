namespace HunterSim
{
    public class ImprovedAspectOfTheHawkExpiredEvent : EventInfo
    {
        public ImprovedAspectOfTheHawkExpiredEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state) => state.Auras.Remove(Aura.ImprovedAspectOfTheHawk);
    }
}
