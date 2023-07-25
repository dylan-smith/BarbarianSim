using System.Data;
using BarbarianSim.Config;
using BarbarianSim.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BarbarianSim;

public class Simulation
{
    public SimulationState State { get; init; }
    private readonly ServiceProvider _sp;

    public Simulation(SimulationConfig config, ServiceProvider sp)
    {
        State = new SimulationState(config);
        _sp = sp;
    }

    public SimulationState Run()
    {
        if (!State.Validate())
        {
            return State;
        }

        var eventPublisher = _sp.GetRequiredService<EventPublisher>();
        eventPublisher.PublishEvent(new SimulationStartedEvent(0.0), State);

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
                eventPublisher.PublishEvent(nextEvent, State);
                nextEvent = GetNextEvent();
            }
        }

        throw new Exception("This should never happen");
    }

    private Events.EventInfo GetNextEvent() => State.Events.OrderBy(e => e.Timestamp).FirstOrDefault();
}
