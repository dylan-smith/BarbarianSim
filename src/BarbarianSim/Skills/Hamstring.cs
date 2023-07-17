using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class Hamstring
{
    public void ProcessEvent(BleedAppliedEvent bleedAppliedEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.Hamstring))
        {
            bleedAppliedEvent.Target.Auras.Add(Aura.Slow);
            var expiryTime = bleedAppliedEvent.BleedCompletedEvent.Timestamp;
            state.Events.Add(new AuraExpiredEvent(expiryTime, Aura.Slow));
        }
    }
}
