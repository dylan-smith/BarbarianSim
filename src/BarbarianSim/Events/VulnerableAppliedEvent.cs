using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class VulnerableAppliedEvent : EventInfo
{
    public VulnerableAppliedEvent(double timestamp, EnemyState target, double duration) : base(timestamp)
    {
        Duration = duration;
        Target = target;
    }

    public double Duration { get; set; }
    public EnemyState Target { get; init; }

    public AuraExpiredEvent VulnerableExpiredEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        Target.Auras.Add(Aura.Vulnerable);

        VulnerableExpiredEvent = new AuraExpiredEvent(Timestamp + Duration, Target, Aura.Vulnerable);
        state.Events.Add(VulnerableExpiredEvent);
    }
}
