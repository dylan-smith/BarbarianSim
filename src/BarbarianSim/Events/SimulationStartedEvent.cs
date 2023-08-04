namespace BarbarianSim.Events;

public class SimulationStartedEvent : EventInfo
{
    public SimulationStartedEvent(double timestamp) : base(timestamp, null)
    {
    }
}
