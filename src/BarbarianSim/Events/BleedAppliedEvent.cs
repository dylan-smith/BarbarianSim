using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events;

public class BleedAppliedEvent : EventInfo
{
    public BleedAppliedEvent(BleedCompletedEventFactory bleedCompletedEventFactory, double timestamp, double damage, double duration, EnemyState target) : base(timestamp)
    {
        _bleedCompletedEventFactory = bleedCompletedEventFactory;
        Damage = damage;
        Duration = duration;
        Target = target;
    }

    private readonly BleedCompletedEventFactory _bleedCompletedEventFactory;

    public double Damage { get; init; }
    public double Duration { get; init; }
    public EnemyState Target { get; init; }
    public BleedCompletedEvent BleedCompletedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        Target.Auras.Add(Aura.Bleeding);
        BleedCompletedEvent = _bleedCompletedEventFactory.Create(Timestamp + Duration, Damage, Target);
        state.Events.Add(BleedCompletedEvent);
    }
}
