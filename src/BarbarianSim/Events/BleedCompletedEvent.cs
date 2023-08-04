namespace BarbarianSim.Events;

public class BleedCompletedEvent : EventInfo
{
    public BleedCompletedEvent(double timestamp, string source, double damage, EnemyState target) : base(timestamp, source)
    {
        Damage = damage;
        Target = target;
    }

    public double Damage { get; init; }
    public DamageEvent DamageEvent { get; set; }
    public EnemyState Target { get; init; }

    public override string ToString() => $"{base.ToString()} - Bleed completed, {Damage:F2} damage dealt to Enemy #{Target.Id} (Source: {Source})";
}
