namespace BarbarianSim.Events;

public class BleedAppliedEvent : EventInfo
{
    public BleedAppliedEvent(double timestamp, string source, double damage, double duration, EnemyState target) : base(timestamp, source)
    {
        Damage = damage;
        Duration = duration;
        Target = target;
    }

    public double Damage { get; init; }
    public double Duration { get; init; }
    public EnemyState Target { get; init; }
    public BleedCompletedEvent BleedCompletedEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - Bleed applied for {Damage:F2} damage over {Duration:F2} seconds (Source: {Source})";
}
