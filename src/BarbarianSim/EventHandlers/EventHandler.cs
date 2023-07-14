using System.Diagnostics.CodeAnalysis;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public abstract class EventHandler<T> where T : EventInfo
{
    public abstract void ProcessEvent(T e, SimulationState state);
}
