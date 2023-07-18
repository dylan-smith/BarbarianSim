using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class RaidLeader : IHandlesEvent<Events.ChallengingShoutEvent>
{
    // Your Shouts also heal Allies for X% of their Maximum Life per second
    public virtual double GetHealPercentage(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.TryGetValue(Skill.RaidLeader, out var pointsSpent))
        {
            skillPoints += pointsSpent;
        }

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
}
