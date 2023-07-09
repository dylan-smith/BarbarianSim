using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class WrathOfTheBerserkerEvent : EventInfo
{
    public WrathOfTheBerserkerEvent(double timestamp) : base(timestamp)
    {
    }

    public CooldownCompletedEvent WrathOfTheBerserkerCooldownCompletedEvent { get; set; }
    public AuraExpiredEvent WrathOfTheBerserkerExpiredEvent { get; set; }
    public AuraExpiredEvent UnstoppableExpiredEvent { get; set; }
    public BerserkingAppliedEvent BerserkingAppliedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.WrathOfTheBerserker);
        state.Player.Auras.Add(Aura.WrathOfTheBerserkerCooldown);
        state.Player.Auras.Add(Aura.Unstoppable);

        BerserkingAppliedEvent = new BerserkingAppliedEvent(Timestamp, WrathOfTheBerserker.BERSERKING_DURATION);
        state.Events.Add(BerserkingAppliedEvent);

        WrathOfTheBerserkerExpiredEvent = new AuraExpiredEvent(Timestamp + WrathOfTheBerserker.DURATION, Aura.WrathOfTheBerserker);
        state.Events.Add(WrathOfTheBerserkerExpiredEvent);

        WrathOfTheBerserkerCooldownCompletedEvent = new CooldownCompletedEvent(Timestamp + WrathOfTheBerserker.COOLDOWN, Aura.WrathOfTheBerserkerCooldown);
        state.Events.Add(WrathOfTheBerserkerCooldownCompletedEvent);

        UnstoppableExpiredEvent = new AuraExpiredEvent(Timestamp + WrathOfTheBerserker.UNSTOPPABLE_DURATION, Aura.Unstoppable);
        state.Events.Add(UnstoppableExpiredEvent);
    }
}
