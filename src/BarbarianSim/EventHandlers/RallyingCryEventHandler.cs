using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class RallyingCryEventHandler : EventHandler<RallyingCryEvent>
{
    public RallyingCryEventHandler(BoomingVoice boomingVoice) => _boomingVoice = boomingVoice;

    private readonly BoomingVoice _boomingVoice;

    public override void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        e.Duration = RallyingCry.DURATION * _boomingVoice.GetDurationIncrease(state);

        e.RallyingCryAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Rallying Cry", e.Duration, Aura.RallyingCry);
        state.Events.Add(e.RallyingCryAuraAppliedEvent);

        e.RallyingCryCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Rallying Cry", RallyingCry.COOLDOWN, Aura.RallyingCryCooldown);
        state.Events.Add(e.RallyingCryCooldownAuraAppliedEvent);
    }
}
