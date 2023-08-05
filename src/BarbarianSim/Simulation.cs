using System.Data;
using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim;

public class Simulation
{
    public SimulationState State { get; init; }
    private readonly EventPublisher _eventPublisher;
    private readonly SimLogger _log;

    public Simulation(SimulationConfig config, EventPublisher eventPublisher, SimLogger log)
    {
        State = new SimulationState(config);
        _eventPublisher = eventPublisher;
        _log = log;
    }

    public SimulationState Run()
    {
        if (!State.Validate())
        {
            return State;
        }

        _eventPublisher.PublishEvent(new SimulationStartedEvent(0.0), State);

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
                _log.ActiveEvent = nextEvent;
                _eventPublisher.PublishEvent(nextEvent, State);
                nextEvent = GetNextEvent();
            }
        }

        throw new Exception("This should never happen");
    }

    private EventInfo GetNextEvent() => State.Events.OrderBy(e => e.Timestamp).FirstOrDefault();
}
