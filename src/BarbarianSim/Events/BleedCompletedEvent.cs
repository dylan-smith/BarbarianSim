using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events;

public class BleedCompletedEvent : EventInfo
{
    public BleedCompletedEvent(DamageEventFactory damageEventFactory, double timestamp, double damage, EnemyState target) : base(timestamp)
    {
        _damageEventFactory = damageEventFactory;
        Damage = damage;
        Target = target;
    }

    private readonly DamageEventFactory _damageEventFactory;

    public double Damage { get; init; }
    public DamageEvent DamageEvent { get; set; }
    public EnemyState Target { get; init; }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Events.Any(e => e is BleedCompletedEvent))
        {
            Target.Auras.Remove(Aura.Bleeding);
        }

        DamageEvent = _damageEventFactory.Create(Timestamp, Damage, DamageType.DamageOverTime, DamageSource.Bleeding, SkillType.None, Target);
        state.Events.Add(DamageEvent);
    }
}
