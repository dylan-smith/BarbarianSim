using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class SimulationStartedEventHandler : EventHandler<SimulationStartedEvent>
{
    public override void ProcessEvent(SimulationStartedEvent e, SimulationState state)
    {
        // Do Nothing
    }
}
