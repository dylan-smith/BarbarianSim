using BarbarianSim.Events;

namespace BarbarianSim;

public interface IHandlesEvent<in T> where T : EventInfo
{
    void ProcessEvent(T e, SimulationState state);
}
