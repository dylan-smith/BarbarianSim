namespace BarbarianSim.Events;

public class PressurePointProcEvent : EventInfo
{
    public PressurePointProcEvent(double timestamp, EnemyState target) : base(timestamp) => Target = target;

    public EnemyState Target { get; init; }
    public AuraAppliedEvent VulnerableAppliedEvent { get; set; }
}
