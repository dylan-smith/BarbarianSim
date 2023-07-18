using System.Data;
using System.Reflection;
using BarbarianSim.Config;
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
                ProcessEvent(nextEvent, State);

                eventPublisher.PublishEvent(nextEvent, State);

                nextEvent = GetNextEvent();
            }
        }

        throw new Exception("This should never happen");
    }

    private void ProcessEvent(Events.EventInfo nextEvent, SimulationState state)
    {
        GetType().GetMethod(nameof(ProcessEventImpl), BindingFlags.NonPublic | BindingFlags.Instance)
                 .MakeGenericMethod(nextEvent.GetType())
                 .Invoke(this, new object[] { nextEvent, state });
    }

    private void ProcessEventImpl<T>(T nextEvent, SimulationState state) where T : Events.EventInfo
    {
        var handler = _sp.GetRequiredService<EventHandlers.EventHandler<T>>();
        handler.ProcessEvent(nextEvent, state);
    }

    private Events.EventInfo GetNextEvent() => State.Events.OrderBy(e => e.Timestamp).FirstOrDefault();
}
