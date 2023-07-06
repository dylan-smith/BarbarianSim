using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class BerserkingExpiredEvent : EventInfo
{
    public BerserkingExpiredEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        // if there are other events it means there's a berserking been applied with a later expiration time
        if (!state.Events.Any(e => e is BerserkingExpiredEvent))
        {
            state.Player.Auras.Remove(Aura.Berserking);
        }
    }
}
