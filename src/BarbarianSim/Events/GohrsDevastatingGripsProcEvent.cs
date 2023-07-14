using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class GohrsDevastatingGripsProcEvent : EventInfo
{
    public GohrsDevastatingGripsProcEvent(double timestamp, double damage) : base(timestamp) => Damage = damage;

    public double Damage { get; init; }
    public IList<DamageEvent> DamageEvents { get; init; } = new List<DamageEvent>();
}
