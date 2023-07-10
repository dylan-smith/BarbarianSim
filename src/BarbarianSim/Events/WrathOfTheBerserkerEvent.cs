using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class WrathOfTheBerserkerEvent : EventInfo
{
    public WrathOfTheBerserkerEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent WrathOfTheBerserkerAuraAppliedEvent { get; set; }
    public AuraAppliedEvent UnstoppableAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WrathOfTheBerserkerCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        WrathOfTheBerserkerAuraAppliedEvent = new AuraAppliedEvent(Timestamp, WrathOfTheBerserker.DURATION, Aura.WrathOfTheBerserker);
        state.Events.Add(WrathOfTheBerserkerAuraAppliedEvent);

        UnstoppableAuraAppliedEvent = new AuraAppliedEvent(Timestamp, WrathOfTheBerserker.UNSTOPPABLE_DURATION, Aura.Unstoppable);
        state.Events.Add(UnstoppableAuraAppliedEvent);

        BerserkingAuraAppliedEvent = new AuraAppliedEvent(Timestamp, WrathOfTheBerserker.BERSERKING_DURATION, Aura.Berserking);
        state.Events.Add(BerserkingAuraAppliedEvent);

        WrathOfTheBerserkerCooldownAuraAppliedEvent = new AuraAppliedEvent(Timestamp, WrathOfTheBerserker.COOLDOWN, Aura.WrathOfTheBerserkerCooldown);
        state.Events.Add(WrathOfTheBerserkerCooldownAuraAppliedEvent);
    }
}
