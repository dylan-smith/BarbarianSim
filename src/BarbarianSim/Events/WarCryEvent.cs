using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class WarCryEvent : EventInfo
{
    public WarCryEvent(double timestamp) : base(timestamp)
    {
    }

    public CooldownCompletedEvent WarCryCooldownCompletedEvent { get; set; }
    public AuraExpiredEvent WarCryExpiredEvent { get; set; }
    public BerserkingAppliedEvent BerserkingAppliedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.WarCry);
        state.Player.Auras.Add(Aura.WarCryCooldown);

        var duration = WarCry.DURATION * BoomingVoice.GetDurationIncrease(state);

        WarCryCooldownCompletedEvent = new CooldownCompletedEvent(Timestamp + WarCry.COOLDOWN, Aura.WarCryCooldown);
        state.Events.Add(WarCryCooldownCompletedEvent);

        WarCryExpiredEvent = new AuraExpiredEvent(Timestamp + duration, Aura.WarCry);
        state.Events.Add(WarCryExpiredEvent);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedWarCry))
        {
            BerserkingAppliedEvent = new BerserkingAppliedEvent(Timestamp, WarCry.BERSERKING_DURATION_FROM_ENHANCED);
            state.Events.Add(BerserkingAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.MightyWarCry))
        {
            FortifyGeneratedEvent = new FortifyGeneratedEvent(Timestamp, WarCry.FORTIFY_FROM_MIGHTY * state.Player.BaseLife);
            state.Events.Add(FortifyGeneratedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = new RaidLeaderProcEvent(Timestamp, duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
