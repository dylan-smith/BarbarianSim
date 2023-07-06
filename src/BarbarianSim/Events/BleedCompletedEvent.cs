using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class BleedCompletedEvent : EventInfo
{
    public BleedCompletedEvent(double timestamp, double damage) : base(timestamp) => Damage = damage;

    public double Damage { get; init; }
    public DamageEvent DamageEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Events.Any(e => e is BleedCompletedEvent))
        {
            state.Player.Auras.Remove(Aura.Bleeding);
        }

        DamageEvent = new DamageEvent(Timestamp, Damage, DamageType.DamageOverTime, DamageSource.Bleeding);
        state.Events.Add(DamageEvent);
    }
}
