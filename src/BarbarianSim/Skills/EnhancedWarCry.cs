using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class EnhancedWarCry : IHandlesEvent<WarCryEvent>
{
    // War Cry grants you Berserking for 4 seconds
    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.EnhancedWarCry) && state.Config.Skills[Skill.EnhancedWarCry] > 0)
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Enhanced War Cry", WarCry.BERSERKING_DURATION_FROM_ENHANCED, Aura.Berserking));
        }
    }
}
