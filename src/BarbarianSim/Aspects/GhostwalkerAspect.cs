using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class GhostwalkerAspect : Aspect
{
    public GhostwalkerAspect(AuraAppliedEventFactory auraAppliedEventFactory, int speed)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        Speed = speed;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    // While Unstoppable and for 4 seconds after, you gain 10-25%[+] increased Movement Speed and can move freely through enemies
    public int Speed { get; init; }

    public void ProcessEvent(AuraAppliedEvent auraAppliedEvent, SimulationState state)
    {
        if (auraAppliedEvent.Aura == Aura.Unstoppable)
        {
            state.Events.Add(_auraAppliedEventFactory.Create(auraAppliedEvent.Timestamp, auraAppliedEvent.Duration + 4.0, Aura.Ghostwalker));
        }
    }
}
