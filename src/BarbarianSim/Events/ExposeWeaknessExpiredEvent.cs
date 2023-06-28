namespace BarbarianSim
{
    public class ExposeWeaknessExpiredEvent : EventInfo
    {
        public ExposeWeaknessExpiredEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state) => state.Auras.Remove(Aura.ExposeWeakness);
    }
}
