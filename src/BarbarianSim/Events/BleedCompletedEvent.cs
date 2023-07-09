using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class BleedCompletedEvent : EventInfo
{
    public BleedCompletedEvent(double timestamp, double damage, EnemyState target) : base(timestamp)
    {
        Damage = damage;
        Target = target;
    }

    public double Damage { get; init; }
    public DamageEvent DamageEvent { get; set; }
    public EnemyState Target { get; init; }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Events.Any(e => e is BleedCompletedEvent))
        {
            Target.Auras.Remove(Aura.Bleeding);
        }

        DamageEvent = new DamageEvent(Timestamp, Damage, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, Target);
        state.Events.Add(DamageEvent);
    }
}
