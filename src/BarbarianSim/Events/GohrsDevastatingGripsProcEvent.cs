namespace BarbarianSim.Events;

[Proc("Gohr's Devastating Grips")]
public class GohrsDevastatingGripsProcEvent : EventInfo
{
    public GohrsDevastatingGripsProcEvent(double timestamp, double damage) : base(timestamp, null) => Damage = damage;

    public double Damage { get; init; }
    public IList<DamageEvent> DamageEvents { get; init; } = new List<DamageEvent>();

    public override string ToString() => $"{base.ToString()} - {Damage:F2} damage";
}
