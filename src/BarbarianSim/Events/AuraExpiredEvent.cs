using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class AuraExpiredEvent : EventInfo
{
    public AuraExpiredEvent(double timestamp, string source, Aura aura) : base(timestamp, source) => Aura = aura;

    public AuraExpiredEvent(double timestamp, string source, EnemyState target, Aura aura) : base(timestamp, source)
    {
        Aura = aura;
        Target = target;
    }

    public Aura Aura { get; set; }
    public EnemyState Target { get; set; }

    public override string ToString() => $"{base.ToString()} - {Aura} expired (Source: {Source})";
}
