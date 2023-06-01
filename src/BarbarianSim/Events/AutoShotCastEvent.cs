namespace HunterSim
{
    public class AutoShotCastEvent : EventInfo
    {
        public AutoShotCastEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            var autoShotSpeed = state.Config.Gear.Ranged.Speed / RangedHasteCalculator.Calculate(state);
            var castTime = 0.5 / RangedHasteCalculator.Calculate(state);
            state.Events.Add(new AutoShotCompletedEvent(Timestamp + castTime));
            // TODO: shouldn't the cooldown start on the completed event?
            state.Auras.Add(Aura.AutoShotOnCooldown);
            state.Events.Add(new AutoShotCooldownCompletedEvent(Timestamp + autoShotSpeed));
        }
    }
}
