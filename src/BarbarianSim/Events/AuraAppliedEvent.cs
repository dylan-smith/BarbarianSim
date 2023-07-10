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

    public override void ProcessEvent(SimulationState state)
    {
        if (Target == null)
        {
            state.Player.Auras.Add(Aura);
        }
        else
        {
            Target.Auras.Add(Aura);
        }

        if (Duration > 0)
        {
            AuraExpiredEvent = new AuraExpiredEvent(Timestamp + Duration, Target, Aura);
            state.Events.Add(AuraExpiredEvent);
        }
    }
}
