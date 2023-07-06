using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class RallyingCryEvent : EventInfo
{
    public RallyingCryEvent(double timestamp) : base(timestamp)
    {
    }

    public RallyingCryCooldownCompletedEvent RallyingCryCooldownCompletedEvent { get; set; }
    public RallyingCryExpiredEvent RallyingCryExpiredEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Player.Auras.Add(Aura.RallyingCryCooldown);

        RallyingCryCooldownCompletedEvent = new RallyingCryCooldownCompletedEvent(Timestamp + RallyingCry.COOLDOWN);
        state.Events.Add(RallyingCryCooldownCompletedEvent);

        RallyingCryExpiredEvent = new RallyingCryExpiredEvent(Timestamp + RallyingCry.DURATION);
        state.Events.Add(RallyingCryExpiredEvent);
    }
}
