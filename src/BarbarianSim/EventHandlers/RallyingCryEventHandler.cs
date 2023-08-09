using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class RallyingCryEventHandler : EventHandler<RallyingCryEvent>
{
    public RallyingCryEventHandler(BoomingVoice boomingVoice, SimLogger log)
    {
        _boomingVoice = boomingVoice;
        _log = log;
    }

    private readonly BoomingVoice _boomingVoice;
    private readonly SimLogger _log;

    public override void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        e.Duration = RallyingCry.DURATION * _boomingVoice.GetDurationIncrease(state);

        e.RallyingCryAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Rallying Cry", e.Duration, Aura.RallyingCry);
        state.Events.Add(e.RallyingCryAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Rallying Cry for {e.Duration:F2} seconds");

        e.RallyingCryCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Rallying Cry", RallyingCry.COOLDOWN, Aura.RallyingCryCooldown);
        state.Events.Add(e.RallyingCryCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Rallying Cry Cooldown for {RallyingCry.COOLDOWN:F2} seconds");
    }
}
