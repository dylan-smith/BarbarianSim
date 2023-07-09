using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class RallyingCryEvent : EventInfo
{
    public RallyingCryEvent(double timestamp) : base(timestamp)
    {
    }

    public CooldownCompletedEvent RallyingCryCooldownCompletedEvent { get; set; }
    public AuraExpiredEvent RallyingCryExpiredEvent { get; set; }
    public AuraExpiredEvent UnstoppableExpiredEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Player.Auras.Add(Aura.RallyingCryCooldown);

        Duration = RallyingCry.DURATION * BoomingVoice.GetDurationIncrease(state);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedRallyingCry))
        {
            state.Player.Auras.Add(Aura.Unstoppable);
            UnstoppableExpiredEvent = new AuraExpiredEvent(Timestamp + Duration, Aura.Unstoppable);
            state.Events.Add(UnstoppableExpiredEvent);
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

        RallyingCryExpiredEvent = new AuraExpiredEvent(Timestamp + Duration, Aura.RallyingCry);
        state.Events.Add(RallyingCryExpiredEvent);

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = new RaidLeaderProcEvent(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
