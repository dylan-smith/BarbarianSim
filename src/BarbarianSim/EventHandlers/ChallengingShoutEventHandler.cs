using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class ChallengingShoutEventHandler : EventHandler<ChallengingShoutEvent>
{
    public ChallengingShoutEventHandler(BoomingVoice boomingVoice) => _boomingVoice = boomingVoice;

    private readonly BoomingVoice _boomingVoice;

    public override void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        e.Duration = ChallengingShout.DURATION * _boomingVoice.GetDurationIncrease(state);

        e.ChallengingShoutAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.ChallengingShout);
        state.Events.Add(e.ChallengingShoutAuraAppliedEvent);

        e.ChallengingShoutCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, ChallengingShout.COOLDOWN, Aura.ChallengingShoutCooldown);
        state.Events.Add(e.ChallengingShoutCooldownAuraAppliedEvent);

        foreach (var enemy in state.Enemies)
        {
            var tauntAppliedEvent = new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.Taunt, enemy);
            e.TauntAuraAppliedEvent.Add(tauntAppliedEvent);
            state.Events.Add(tauntAppliedEvent);
        }
    }
}
