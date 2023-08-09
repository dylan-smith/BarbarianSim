using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WrathOfTheBerserkerEventHandler : EventHandler<WrathOfTheBerserkerEvent>
{
    public WrathOfTheBerserkerEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(WrathOfTheBerserkerEvent e, SimulationState state)
    {
        e.WrathOfTheBerserkerAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.DURATION, Aura.WrathOfTheBerserker);
        state.Events.Add(e.WrathOfTheBerserkerAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Wrath of the Berserker for {WrathOfTheBerserker.DURATION:F2} seconds");

        e.UnstoppableAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.UNSTOPPABLE_DURATION, Aura.Unstoppable);
        state.Events.Add(e.UnstoppableAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Unstoppable for {WrathOfTheBerserker.UNSTOPPABLE_DURATION:F2} seconds");

        e.BerserkingAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.BERSERKING_DURATION, Aura.Berserking);
        state.Events.Add(e.BerserkingAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Berserking for {WrathOfTheBerserker.BERSERKING_DURATION:F2} seconds");

        e.WrathOfTheBerserkerCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Wrath of the Berserker", WrathOfTheBerserker.COOLDOWN, Aura.WrathOfTheBerserkerCooldown);
        state.Events.Add(e.WrathOfTheBerserkerCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Wrath of the Berserker Cooldown for {WrathOfTheBerserker.COOLDOWN:F2} seconds");
    }
}
