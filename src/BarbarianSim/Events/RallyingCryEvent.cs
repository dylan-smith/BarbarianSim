using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class RallyingCryEvent : EventInfo
{
    public RallyingCryEvent(double timestamp) : base(timestamp)
    {
    }

    public RallyingCryCooldownCompletedEvent RallyingCryCooldownCompletedEvent { get; set; }
    public RallyingCryExpiredEvent RallyingCryExpiredEvent { get; set; }
    public UnstoppableExpiredEvent UnstoppableExpiredEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Player.Auras.Add(Aura.RallyingCryCooldown);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedRallyingCry))
        {
            state.Player.Auras.Add(Aura.Unstoppable);
            UnstoppableExpiredEvent = new UnstoppableExpiredEvent(Timestamp + RallyingCry.DURATION);
            state.Events.Add(UnstoppableExpiredEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.TacticalRallyingCry))
        {
            FuryGeneratedEvent = new FuryGeneratedEvent(Timestamp, RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY);
            state.Events.Add(FuryGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry))
        {
            FortifyGeneratedEvent = new FortifyGeneratedEvent(Timestamp, RallyingCry.FORTIFY_FROM_STRATEGIC_RALLYING_CRY * state.Player.MaxLife);
            state.Events.Add(FortifyGeneratedEvent);
        }

        RallyingCryCooldownCompletedEvent = new RallyingCryCooldownCompletedEvent(Timestamp + RallyingCry.COOLDOWN);
        state.Events.Add(RallyingCryCooldownCompletedEvent);

        RallyingCryExpiredEvent = new RallyingCryExpiredEvent(Timestamp + RallyingCry.DURATION);
        state.Events.Add(RallyingCryExpiredEvent);
    }
}
