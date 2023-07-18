using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class GhostwalkerAspect : Aspect, IHandlesEvent<AuraAppliedEvent>
{
    // While Unstoppable and for 4 seconds after, you gain 10-25%[+] increased Movement Speed and can move freely through enemies
    public int Speed { get; set; }

    public void ProcessEvent(AuraAppliedEvent auraAppliedEvent, SimulationState state)
    {
        if (auraAppliedEvent.Aura == Aura.Unstoppable)
        {
            state.Events.Add(new AuraAppliedEvent(auraAppliedEvent.Timestamp, auraAppliedEvent.Duration + 4.0, Aura.Ghostwalker));
        }
    }
}
