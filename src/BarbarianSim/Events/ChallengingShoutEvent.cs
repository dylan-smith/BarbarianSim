using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class ChallengingShoutEvent : EventInfo
{
    public ChallengingShoutEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraExpiredEvent ChallengingShoutCooldownCompletedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }
    public AuraAppliedEvent ChallengingShoutAuraAppliedEvent { get; set; }
    public IList<AuraAppliedEvent> TauntAuraAppliedEvent { get; init; } = new List<AuraAppliedEvent>();

    public override void ProcessEvent(SimulationState state)
    {
        Duration = ChallengingShout.DURATION * BoomingVoice.GetDurationIncrease(state);

        ChallengingShoutAuraAppliedEvent = new AuraAppliedEvent(Timestamp, Duration, Aura.ChallengingShout);
        state.Events.Add(ChallengingShoutAuraAppliedEvent);

        state.Player.Auras.Add(Aura.ChallengingShoutCooldown);
        ChallengingShoutCooldownCompletedEvent = new AuraExpiredEvent(Timestamp + ChallengingShout.COOLDOWN, Aura.ChallengingShoutCooldown);
        state.Events.Add(ChallengingShoutCooldownCompletedEvent);

        foreach (var enemy in state.Enemies)
        {
            var tauntAppliedEvent = new AuraAppliedEvent(Timestamp, Duration, Aura.Taunt, enemy);
            TauntAuraAppliedEvent.Add(tauntAppliedEvent);
            state.Events.Add(tauntAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = new RaidLeaderProcEvent(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
