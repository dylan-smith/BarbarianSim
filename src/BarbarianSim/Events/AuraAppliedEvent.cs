using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events;

public class AuraAppliedEvent : EventInfo
{
    public AuraAppliedEvent(AuraExpiredEventFactory auraExpiredEventFactory, double timestamp, double duration, Aura aura) : base(timestamp)
    {
        _auraExpiredEventFactory = auraExpiredEventFactory;
        Duration = duration;
        Aura = aura;
    }

    public AuraAppliedEvent(AuraExpiredEventFactory auraExpiredEventFactory, double timestamp, double duration, Aura aura, EnemyState target) : base(timestamp)
    {
        _auraExpiredEventFactory = auraExpiredEventFactory;
        Duration = duration;
        Aura = aura;
        Target = target;
    }

    private readonly AuraExpiredEventFactory _auraExpiredEventFactory;

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
            AuraExpiredEvent = _auraExpiredEventFactory.Create(Timestamp + Duration, Target, Aura);
            state.Events.Add(AuraExpiredEvent);
        }
    }

    public override string ToString() => $"[{Timestamp:F1}] {Aura} Aura Applied";
}
