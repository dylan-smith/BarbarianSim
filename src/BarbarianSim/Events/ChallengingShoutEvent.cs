using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class ChallengingShoutEvent : EventInfo
{
    public ChallengingShoutEvent(double timestamp) : base(timestamp)
    {
    }

    public ChallengingShoutCooldownCompletedEvent ChallengingShoutCooldownCompletedEvent { get; set; }
    public ChallengingShoutExpiredEvent ChallengingShoutExpiredEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.ChallengingShout);
        state.Player.Auras.Add(Aura.ChallengingShoutCooldown);

        ChallengingShoutCooldownCompletedEvent = new ChallengingShoutCooldownCompletedEvent(Timestamp + ChallengingShout.COOLDOWN);
        state.Events.Add(ChallengingShoutCooldownCompletedEvent);

        ChallengingShoutExpiredEvent = new ChallengingShoutExpiredEvent(Timestamp + ChallengingShout.DURATION);
        state.Events.Add(ChallengingShoutExpiredEvent);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Add(Aura.Taunt);
        }
    }
}
