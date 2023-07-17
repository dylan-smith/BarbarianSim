using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class Hamstring
{
    public void ProcessEvent(BleedAppliedEvent bleedAppliedEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.Hamstring))
        {
            state.Events.Add(new AuraAppliedEvent(bleedAppliedEvent.Timestamp, bleedAppliedEvent.Duration, Aura.Slow));
        }
    }
}
