using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class ChallengingShoutEvent : EventInfo
{
    public ChallengingShoutEvent(double timestamp) : base(timestamp)
    {
    }

    public CooldownCompletedEvent ChallengingShoutCooldownCompletedEvent { get; set; }
    public ChallengingShoutExpiredEvent ChallengingShoutExpiredEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.ChallengingShout);
        state.Player.Auras.Add(Aura.ChallengingShoutCooldown);

        ChallengingShoutCooldownCompletedEvent = new CooldownCompletedEvent(Timestamp + ChallengingShout.COOLDOWN, Aura.ChallengingShoutCooldown);
        state.Events.Add(ChallengingShoutCooldownCompletedEvent);

        Duration = ChallengingShout.DURATION * BoomingVoice.GetDurationIncrease(state);

        ChallengingShoutExpiredEvent = new ChallengingShoutExpiredEvent(Timestamp + Duration);
        state.Events.Add(ChallengingShoutExpiredEvent);

        foreach (var enemy in state.Enemies)
        {
            enemy.Auras.Add(Aura.Taunt);
        }

        if (state.Config.Skills.ContainsKey(Skill.RaidLeader))
        {
            RaidLeaderProcEvent = new RaidLeaderProcEvent(Timestamp, Duration);
            state.Events.Add(RaidLeaderProcEvent);
        }
    }
}
