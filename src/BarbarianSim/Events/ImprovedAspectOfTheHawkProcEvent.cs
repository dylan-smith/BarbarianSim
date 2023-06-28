namespace BarbarianSim
{
    public class ImprovedAspectOfTheHawkProcEvent : EventInfo
    {
        public ImprovedAspectOfTheHawkProcEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            if (state.Auras.Contains(Aura.ImprovedAspectOfTheHawk))
            {
                state.Auras.Remove(Aura.ImprovedAspectOfTheHawk);
                state.Events.RemoveAll(x => x is ImprovedAspectOfTheHawkExpiredEvent);
            }

            state.Auras.Add(Aura.ImprovedAspectOfTheHawk);
            state.Events.Add(new ImprovedAspectOfTheHawkExpiredEvent(Timestamp + 12));
        }
    }
}
