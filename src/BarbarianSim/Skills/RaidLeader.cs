using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class RaidLeader : IHandlesEvent<ChallengingShoutEvent>, IHandlesEvent<RallyingCryEvent>, IHandlesEvent<WarCryEvent>
{
    // Your Shouts also heal Allies for X% of their Maximum Life per second
    public virtual double GetHealPercentage(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.RaidLeader);

        return skillPoints switch
        {
            1 => 0.01,
            2 => 0.02,
            >= 3 => 0.03,
            _ => 0,
        };
    }

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.RaidLeader) && state.Config.Skills[Skill.RaidLeader] > 0)
        {
            state.Events.Add(new RaidLeaderProcEvent(e.Timestamp, e.Duration));
        }
    }

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.RaidLeader) && state.Config.Skills[Skill.RaidLeader] > 0)
        {
            state.Events.Add(new RaidLeaderProcEvent(e.Timestamp, e.Duration));
        }
    }

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.RaidLeader) && state.Config.Skills[Skill.RaidLeader] > 0)
        {
            state.Events.Add(new RaidLeaderProcEvent(e.Timestamp, e.Duration));
        }
    }
}
