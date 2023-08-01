using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class Hamstring : IHandlesEvent<BleedAppliedEvent>
{
    // Your Bleeding effects Slow Healthy enemies by 10%
    public void ProcessEvent(BleedAppliedEvent e, SimulationState state)
    {
        if (state.Config.HasSkill(Skill.Hamstring) && e.Target.IsHealthy())
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.Slow, e.Target));
        }
    }
}
