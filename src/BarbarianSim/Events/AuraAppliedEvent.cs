using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class AuraAppliedEvent : EventInfo
{
    public AuraAppliedEvent(double timestamp, double duration, Aura aura) : base(timestamp)
    {
        Duration = duration;
        Aura = aura;
    }

    public AuraAppliedEvent(double timestamp, double duration, Aura aura, EnemyState target) : base(timestamp)
    {
        Duration = duration;
        Aura = aura;
        Target = target;
    }

    public double Duration { get; init; }
    public Aura Aura { get; init; }
    public EnemyState Target { get; init; }
    public AuraExpiredEvent AuraExpiredEvent { get; set; }

    public override string ToString() => $"[{Timestamp:F1}] {Aura} Aura Applied";
}
