using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class ChallengingShoutEvent : EventInfo
{
    public ChallengingShoutEvent(double timestamp) : base(timestamp)
    {
    }

    public ChallengingShoutCooldownCompletedEvent ChallengingShoutCooldownCompletedEvent { get; set; }
    public ChallengingShoutExpiredEvent ChallengingShoutExpiredEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.ChallengingShout);
        state.Player.Auras.Add(Aura.ChallengingShoutCooldown);

        ChallengingShoutCooldownCompletedEvent = new ChallengingShoutCooldownCompletedEvent(Timestamp + ChallengingShout.COOLDOWN);
        state.Events.Add(ChallengingShoutCooldownCompletedEvent);

        var duration = ChallengingShout.DURATION * BoomingVoice.GetDurationIncrease(state);

        ChallengingShoutExpiredEvent = new ChallengingShoutExpiredEvent(Timestamp + duration);
        state.Events.Add(ChallengingShoutExpiredEvent);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Add(Aura.Taunt);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = new RaidLeaderProcEvent(Timestamp, duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
