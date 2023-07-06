using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class UnstoppableExpiredEvent : EventInfo
{
    public UnstoppableExpiredEvent(double timestamp) : base(timestamp)
    {
    }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Events.Any(e => e is UnstoppableExpiredEvent))
        {
            state.Player.Auras.Remove(Aura.Unstoppable);
        }
    }
}
