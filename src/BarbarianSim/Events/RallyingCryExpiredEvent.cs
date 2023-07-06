using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class RallyingCryExpiredEvent : EventInfo
{
    public RallyingCryExpiredEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        // if there are other events it means there's a Rallying Cry been applied with a later expiration time (possible if you get the CD shorter than duration)
        if (!state.Events.Any(e => e is RallyingCryExpiredEvent))
        {
            state.Player.Auras.Remove(Aura.RallyingCry);
        }
    }
}
