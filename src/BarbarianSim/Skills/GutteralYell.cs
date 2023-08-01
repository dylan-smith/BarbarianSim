using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class GutteralYell : IHandlesEvent<WarCryEvent>, IHandlesEvent<ChallengingShoutEvent>, IHandlesEvent<RallyingCryEvent>
{
    // Your Shout skills cause enemies to deal X% less damage for 5 seconds
    public const double DURATION = 5;

    public void ProcessEvent(WarCryEvent e, SimulationState state) => state.Events.Add(new GutteralYellProcEvent(e.Timestamp));

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state) => state.Events.Add(new GutteralYellProcEvent(e.Timestamp));

    public void ProcessEvent(RallyingCryEvent e, SimulationState state) => state.Events.Add(new GutteralYellProcEvent(e.Timestamp));

    public virtual double GetDamageReduction(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.GutteralYell);

        return skillPoints switch
        {
            1 => 4,
            2 => 8,
            >= 3 => 12,
            _ => 0,
        };
    }
}
