using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WrathOfTheBerserkerEventHandler : EventHandler<WrathOfTheBerserkerEvent>
{
    public override void ProcessEvent(WrathOfTheBerserkerEvent e, SimulationState state)
    {
        e.WrathOfTheBerserkerAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.DURATION, Aura.WrathOfTheBerserker);
        state.Events.Add(e.WrathOfTheBerserkerAuraAppliedEvent);

        e.UnstoppableAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.UNSTOPPABLE_DURATION, Aura.Unstoppable);
        state.Events.Add(e.UnstoppableAuraAppliedEvent);

        e.BerserkingAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.BERSERKING_DURATION, Aura.Berserking);
        state.Events.Add(e.BerserkingAuraAppliedEvent);

        e.WrathOfTheBerserkerCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.COOLDOWN, Aura.WrathOfTheBerserkerCooldown);
        state.Events.Add(e.WrathOfTheBerserkerCooldownAuraAppliedEvent);
    }
}
