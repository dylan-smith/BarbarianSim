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
}
