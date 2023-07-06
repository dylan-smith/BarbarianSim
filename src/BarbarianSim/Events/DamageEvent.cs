using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class DamageEvent : EventInfo
{
    public double Damage { get; init; }
    public DamageType DamageType { get; init; }
    public DamageSource DamageSource { get; init; }

    public DamageEvent(double timestamp, double damage, DamageType damageType, DamageSource damageSource) : base(timestamp)
    {
        Damage = damage;
        DamageType = damageType;
        DamageSource = damageSource;
    }

    public override void ProcessEvent(SimulationState state) => state.Enemy.Life -= (int)Damage;

    public override string ToString() => $"[{Timestamp:F1}] {DamageType} for {Damage:F2}";
}
