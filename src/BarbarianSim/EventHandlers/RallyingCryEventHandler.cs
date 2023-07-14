using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class RallyingCryEventHandler : EventHandler<RallyingCryEvent>
{
    public override void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        e.Duration = RallyingCry.DURATION * BoomingVoice.GetDurationIncrease(state);

        e.RallyingCryAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.RallyingCry);
        state.Events.Add(e.RallyingCryAuraAppliedEvent);

        e.RallyingCryCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, RallyingCry.COOLDOWN, Aura.RallyingCryCooldown);
        state.Events.Add(e.RallyingCryCooldownAuraAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedRallyingCry))
        {
            e.UnstoppableAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.Unstoppable);
            state.Events.Add(e.UnstoppableAuraAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.TacticalRallyingCry))
        {
            e.FuryGeneratedEvent = new FuryGeneratedEvent(e.Timestamp, RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY);
            state.Events.Add(e.FuryGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry))
        {
            e.FortifyGeneratedEvent = new FortifyGeneratedEvent(e.Timestamp, RallyingCry.FORTIFY_FROM_STRATEGIC_RALLYING_CRY * state.Player.BaseLife);
            state.Events.Add(e.FortifyGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            e.RaidLeaderProcEvent = new RaidLeaderProcEvent(e.Timestamp, e.Duration);
            state.Events.Add(e.RaidLeaderProcEvent);
        }
    }
}
