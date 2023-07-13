using BarbarianSim.Config;
using BarbarianSim.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BarbarianSim;

public class Simulation
{
    public SimulationState State { get; init; }

    public Simulation(SimulationConfig config) => State = new SimulationState(config);

    public SimulationState Run(ServiceProvider sp)
    {
        if (!State.Validate())
        {
            return State;
        }

        while (true)
        {
            State.Config.Rotation.Execute(State);
            var nextEvent = GetNextEvent();

            State.CurrentTime = nextEvent.Timestamp;

            if (State.Enemies.Any(e => e.Life <= 0))
            {
                return State;
            }

            while (nextEvent != null && nextEvent.Timestamp == State.CurrentTime)
            {
                State.Events.Remove(nextEvent);
                nextEvent.ProcessEvent(State);

                EventPublisher.PublishEvent(nextEvent, State, sp);

                nextEvent = GetNextEvent();
            }
        }

        throw new Exception("This should never happen");
    }

    private EventInfo GetNextEvent() => State.Events.OrderBy(e => e.Timestamp).FirstOrDefault();
}
