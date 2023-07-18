using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class Hamstring : IHandlesEvent<BleedAppliedEvent>
{
    public void ProcessEvent(BleedAppliedEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.Hamstring))
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.Slow));
        }
    }
}
