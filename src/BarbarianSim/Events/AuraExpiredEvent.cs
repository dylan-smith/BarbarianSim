using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class AuraExpiredEvent : EventInfo
{
    public AuraExpiredEvent(double timestamp, Aura aura) : base(timestamp) => Aura = aura;

    public AuraExpiredEvent(double timestamp, EnemyState target, Aura aura) : base(timestamp)
    {
        Aura = aura;
        Target = target;
    }

    public Aura Aura { get; set; }
    public EnemyState Target { get; set; }

    public override string ToString() => $"[{Timestamp:F1}] {Aura} Aura Expired";
}
