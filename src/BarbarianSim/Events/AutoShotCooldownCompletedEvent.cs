namespace BarbarianSim
{
    public class AutoShotCooldownCompletedEvent : EventInfo
    {
        public AutoShotCooldownCompletedEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            if (!state.Auras.Remove(Aura.AutoShotOnCooldown))
            {
                // TODO: Richer exceptions
                throw new System.Exception("AutoShotOnCooldown aura was expected in State");
            }
        }
    }
}
