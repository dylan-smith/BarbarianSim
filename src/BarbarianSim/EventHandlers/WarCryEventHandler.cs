using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class WarCryEventHandler : EventHandler<WarCryEvent>
{
    public WarCryEventHandler(BoomingVoice boomingVoice, SimLogger log)
    {
        _boomingVoice = boomingVoice;
        _log = log;
    }

    private readonly BoomingVoice _boomingVoice;
    private readonly SimLogger _log;

    public override void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        e.Duration = WarCry.DURATION * _boomingVoice.GetDurationIncrease(state);

        e.WarCryAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "War Cry", e.Duration, Aura.WarCry);
        state.Events.Add(e.WarCryAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for War Cry for {e.Duration:F2} seconds");

        e.WarCryCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "War Cry", WarCry.COOLDOWN, Aura.WarCryCooldown);
        state.Events.Add(e.WarCryCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for War Cry Cooldown for {WarCry.COOLDOWN:F2} seconds");
    }
}
