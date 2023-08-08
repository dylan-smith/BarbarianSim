using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class ChallengingShoutEventHandler : EventHandler<ChallengingShoutEvent>
{
    public ChallengingShoutEventHandler(BoomingVoice boomingVoice, SimLogger log)
    {
        _boomingVoice = boomingVoice;
        _log = log;
    }

    private readonly BoomingVoice _boomingVoice;
    private readonly SimLogger _log;

    public override void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        e.Duration = ChallengingShout.DURATION;

        var boomingVoiceMultiplier = _boomingVoice.GetDurationIncrease(state);
        if (boomingVoiceMultiplier != 1.0)
        {
            e.Duration *= boomingVoiceMultiplier;
            _log.Verbose($"Booming Voice duration multiplier = {boomingVoiceMultiplier:F2}x");
        }

        e.ChallengingShoutAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Challenging Shout", e.Duration, Aura.ChallengingShout);
        state.Events.Add(e.ChallengingShoutAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Challenging Shout expiring in {e.Duration:F2} seconds");

        e.ChallengingShoutCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Challenging Shout", ChallengingShout.COOLDOWN, Aura.ChallengingShoutCooldown);
        state.Events.Add(e.ChallengingShoutCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Challenging Shout cooldown for {ChallengingShout.COOLDOWN} seconds");

        foreach (var enemy in state.Enemies)
        {
            var tauntAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Challenging Shout", e.Duration, Aura.Taunt, enemy);
            e.TauntAuraAppliedEvent.Add(tauntAppliedEvent);
            state.Events.Add(tauntAppliedEvent);
            _log.Verbose($"Created AuraAppliedEvent for Taunt on Enemy #{enemy.Id} for {e.Duration:F2} seconds");
        }
    }
}
