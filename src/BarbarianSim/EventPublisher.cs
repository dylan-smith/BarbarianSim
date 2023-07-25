using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BarbarianSim;

public class EventPublisher
{
    private readonly Dictionary<Type, IEnumerable<object>> _subscribers = new Dictionary<Type, IEnumerable<object>>();
    private readonly IServiceProvider _serviceProvider;

    public EventPublisher(IServiceProvider sp)
    {
        _serviceProvider = sp;
        var eventTypes = GetEventTypes();

        foreach (var eventType in eventTypes)
        {
            var subscribers = (IEnumerable<object>)typeof(EventPublisher)
                .GetMethod(nameof(GetSubscribers), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(eventType)
                .Invoke(this, new object[] { sp });

            _subscribers.Add(eventType, subscribers.ToList());
        }
    }

    private IEnumerable<Type> GetEventTypes() => typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(Events.EventInfo)));

    public void PublishEvent(Events.EventInfo e, SimulationState state)
    {
        ProcessEvent(e, state, _serviceProvider);
        state.ProcessedEvents.Add(e);

        typeof(EventPublisher).GetMethod(nameof(Publish), BindingFlags.NonPublic | BindingFlags.Instance)
                              .MakeGenericMethod(e.GetType())
                              .Invoke(this, new object[] { e, state });
    }

    private void Publish<TEvent>(TEvent e, SimulationState state) where TEvent : Events.EventInfo
    {
        foreach (var subscriber in _subscribers[typeof(TEvent)])
        {
            ((IHandlesEvent<TEvent>)subscriber).ProcessEvent(e, state);
        }
    }

    private IEnumerable<IHandlesEvent<TEvent>> GetSubscribers<TEvent>(IServiceProvider sp) where TEvent : Events.EventInfo
    {
        var subscribers = GetInterfaceImplementors<IHandlesEvent<TEvent>>(typeof(Program).Assembly);

        foreach (var subscriber in subscribers)
        {
            yield return (IHandlesEvent<TEvent>)sp.GetRequiredService(subscriber);
        }
    }

    private IEnumerable<Type> GetInterfaceImplementors<T>(Assembly assembly) => assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i == typeof(T)));

    private void ProcessEvent(Events.EventInfo nextEvent, SimulationState state, IServiceProvider serviceProvider)
    {
        GetType().GetMethod(nameof(ProcessEventImpl), BindingFlags.NonPublic | BindingFlags.Instance)
                 .MakeGenericMethod(nextEvent.GetType())
                 .Invoke(this, new object[] { nextEvent, state, serviceProvider });
    }

    private void ProcessEventImpl<T>(T nextEvent, SimulationState state, IServiceProvider serviceProvider) where T : Events.EventInfo
    {
        var handler = serviceProvider.GetRequiredService<EventHandlers.EventHandler<T>>();
        handler.ProcessEvent(nextEvent, state);
    }
}
