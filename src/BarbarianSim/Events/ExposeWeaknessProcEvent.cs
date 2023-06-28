using System;

namespace BarbarianSim
{
    public class ExposeWeaknessProcEvent : EventInfo
    {
        public int AttackPower { get; set; }

        public ExposeWeaknessProcEvent(double timestamp) : base(timestamp)
        { }

        public override void ProcessEvent(SimulationState state)
        {
            if (state.Auras.Contains(Aura.ExposeWeakness))
            {
                state.Auras.Remove(Aura.ExposeWeakness);
                state.Events.RemoveAll(x => x is ExposeWeaknessExpiredEvent);
            }

            state.Auras.Add(Aura.ExposeWeakness);
            state.Events.Add(new ExposeWeaknessExpiredEvent(Timestamp + 7));
            AttackPower = (int)Math.Floor(AgilityCalculator.Calculate(state) / 4);
            ExposeWeakness.AttackPower = AttackPower; // Snapshotting agility at time of proc, not sure if this is right, but probably doesn't make a big diff either way
        }
    }
}
