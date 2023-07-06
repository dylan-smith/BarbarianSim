using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class DamageEvent : EventInfo
{
    public double Damage { get; init; }
    public DamageType DamageType { get; init; }
    public DamageSource DamageSource { get; init; }
    public EnemyState Target { get; init; }

    public DamageEvent(double timestamp, double damage, DamageType damageType, DamageSource damageSource, EnemyState target) : base(timestamp)
    {
        Damage = damage;
        DamageType = damageType;
        DamageSource = damageSource;
        Target = target;
    }

    public override void ProcessEvent(SimulationState state) => Target.Life -= (int)Damage;

    public override string ToString() => $"[{Timestamp:F1}] {DamageType} for {Damage:F2}";
}
