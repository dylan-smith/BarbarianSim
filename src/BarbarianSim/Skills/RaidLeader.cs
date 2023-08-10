using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class RaidLeader : IHandlesEvent<ChallengingShoutEvent>, IHandlesEvent<RallyingCryEvent>, IHandlesEvent<WarCryEvent>
{
    // Your Shouts also heal Allies for X% of their Maximum Life per second
    public RaidLeader(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetHealPercentage(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.RaidLeader);

        var result = skillPoints switch
        {
            1 => 0.01,
            2 => 0.02,
            >= 3 => 0.03,
            _ => 0,
        };

        if (result > 0)
        {
            _log.Verbose($"Heal Percentage from Raid Leader = {result:P0} with {skillPoints} skill points");
        }

        return result;
    }

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.RaidLeader) > 0)
        {
            state.Events.Add(new RaidLeaderProcEvent(e.Timestamp, e.Duration));
            _log.Verbose($"Raid Leader created RaidLeaderProcEvent for {e.Duration} seconds");
        }
    }

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.RaidLeader) > 0)
        {
            state.Events.Add(new RaidLeaderProcEvent(e.Timestamp, e.Duration));
            _log.Verbose($"Raid Leader created RaidLeaderProcEvent for {e.Duration} seconds");
        }
    }

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.RaidLeader) > 0)
        {
            state.Events.Add(new RaidLeaderProcEvent(e.Timestamp, e.Duration));
            _log.Verbose($"Raid Leader created RaidLeaderProcEvent for {e.Duration} seconds");
        }
    }
}
