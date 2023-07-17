using BarbarianSim.Events;
using MediatR;

namespace BarbarianSim.EventHandlers;

public abstract class EventHandler<T> : IRequestHandler<T> where T : EventInfo
{
    public abstract Task Handle(T request, CancellationToken cancellationToken);
    public abstract void ProcessEvent(T e, SimulationState state);
}
