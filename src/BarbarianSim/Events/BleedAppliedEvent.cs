using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class BleedAppliedEvent : EventInfo
{
    public BleedAppliedEvent(double timestamp, double damage, double duration) : base(timestamp)
    {
        Damage = damage;
        Duration = duration;
    }

    public double Damage { get; init; }
    public double Duration { get; init; }
    public BleedCompletedEvent BleedCompletedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.Bleeding);
        BleedCompletedEvent = new BleedCompletedEvent(Timestamp + Duration, Damage);
        state.Events.Add(BleedCompletedEvent);
    }
}
