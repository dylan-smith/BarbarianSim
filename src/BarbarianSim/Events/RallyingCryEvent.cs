using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class RallyingCryEvent : EventInfo
{
    public RallyingCryEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent RallyingCryAuraAppliedEvent { get; set; }
    public CooldownCompletedEvent RallyingCryCooldownCompletedEvent { get; set; }
    public AuraAppliedEvent UnstoppableAuraAppliedEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        Duration = RallyingCry.DURATION * BoomingVoice.GetDurationIncrease(state);

        RallyingCryAuraAppliedEvent = new AuraAppliedEvent(Timestamp, Duration, Aura.RallyingCry);
        state.Events.Add(RallyingCryAuraAppliedEvent);

        state.Player.Auras.Add(Aura.RallyingCryCooldown);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedRallyingCry))
        {
            UnstoppableAuraAppliedEvent = new AuraAppliedEvent(Timestamp, Duration, Aura.Unstoppable);
            state.Events.Add(UnstoppableAuraAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.TacticalRallyingCry))
        {
            FuryGeneratedEvent = new FuryGeneratedEvent(Timestamp, RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY);
            state.Events.Add(FuryGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry))
        {
            FortifyGeneratedEvent = new FortifyGeneratedEvent(Timestamp, RallyingCry.FORTIFY_FROM_STRATEGIC_RALLYING_CRY * state.Player.BaseLife);
            state.Events.Add(FortifyGeneratedEvent);
        }

        RallyingCryCooldownCompletedEvent = new CooldownCompletedEvent(Timestamp + RallyingCry.COOLDOWN, Aura.RallyingCryCooldown);
        state.Events.Add(RallyingCryCooldownCompletedEvent);

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = new RaidLeaderProcEvent(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
