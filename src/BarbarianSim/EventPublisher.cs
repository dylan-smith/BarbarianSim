using BarbarianSim.Events;

namespace BarbarianSim;

public static class EventPublisher
{
    public static void PublishEvent(EventInfo e, SimulationState state) => state.ProcessedEvents.Add(e);
}
