using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class EnhancedRallyingCry : IHandlesEvent<RallyingCryEvent>
{
    // Rallying Cry grants you Unstoppable while active
    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.EnhancedRallyingCry) && state.Config.Skills[Skill.EnhancedRallyingCry] > 0)
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Enhanced Rallying Cry", e.Duration, Aura.Unstoppable));
        }
    }
}
