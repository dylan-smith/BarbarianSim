namespace HunterSim
{
    public class MasterTacticianProcEvent : EventInfo
    {
        public MasterTacticianProcEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            if (state.Auras.Contains(Aura.MasterTactician))
            {
                state.Auras.Remove(Aura.MasterTactician);
                state.Events.RemoveAll(x => x is MasterTacticianExpiredEvent);
            }

            state.Auras.Add(Aura.MasterTactician);
            state.Events.Add(new MasterTacticianExpiredEvent(Timestamp + 8));
        }
    }
}
