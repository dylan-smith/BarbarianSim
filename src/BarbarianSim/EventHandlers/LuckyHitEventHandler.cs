using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class LuckyHitEventHandler : EventHandler<LuckyHitEvent>
{
    public override void ProcessEvent(LuckyHitEvent e, SimulationState state)
    {
        // Do nothing, other things will subscribe to this event
    }
}
