using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class TacticalRallyingCry : IHandlesEvent<RallyingCryEvent>
{
    // Rallying Cry generates 20 fury, and grants you an additional 20%[x] Resource Generation
    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.TacticalRallyingCry) && state.Config.Skills[Skill.TacticalRallyingCry] > 0)
        {
            state.Events.Add(new FuryGeneratedEvent(e.Timestamp, RallyingCry.FURY_FROM_TACTICAL_RALLYING_CRY));
        }
    }
}
