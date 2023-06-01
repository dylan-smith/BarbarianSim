namespace HunterSim
{
    public class MasterTacticianExpiredEvent : EventInfo
    {
        public MasterTacticianExpiredEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state) => state.Auras.Remove(Aura.MasterTactician);
    }
}
