namespace BarbarianSim.Events;

public class PressurePointProcEvent : EventInfo
{
    public PressurePointProcEvent(double timestamp, EnemyState target) : base(timestamp, null) => Target = target;

    public EnemyState Target { get; init; }
    public AuraAppliedEvent VulnerableAppliedEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - Applying Vulnerable for 3 seconds to Enemy #{Target.Id}";
}
