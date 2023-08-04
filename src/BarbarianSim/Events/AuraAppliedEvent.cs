using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class AuraAppliedEvent : EventInfo
{
    public AuraAppliedEvent(double timestamp, string source, double duration, Aura aura) : base(timestamp, source)
    {
        Duration = duration;
        Aura = aura;
    }

    public AuraAppliedEvent(double timestamp, string source, double duration, Aura aura, EnemyState target) : base(timestamp, source)
    {
        Duration = duration;
        Aura = aura;
        Target = target;
    }

    public double Duration { get; set; }
    public Aura Aura { get; init; }
    public EnemyState Target { get; init; }
    public AuraExpiredEvent AuraExpiredEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - {Aura} applied for {Duration:F2} seconds (Source: {Source})";
}
