using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class VulnerableExpiredEvent : EventInfo
{
    public VulnerableExpiredEvent(double timestamp, EnemyState target) : base(timestamp) => Target = target;

    public EnemyState Target { get; init; }

    public override void ProcessEvent(SimulationState state)
    {
        // if there are other events it means there's a vulnerable been applied with a later expiration time
        if (!state.Events.OfType<VulnerableExpiredEvent>().Any(e => e.Target == Target))
        {
            Target.Auras.Remove(Aura.Vulnerable);
        }
    }
}
