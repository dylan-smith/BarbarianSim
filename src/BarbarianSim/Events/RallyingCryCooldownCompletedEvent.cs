using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class RallyingCryCooldownCompletedEvent : EventInfo
{
    public RallyingCryCooldownCompletedEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Player.Auras.Remove(Aura.RallyingCryCooldown))
        {
            throw new Exception("RallyingCryCooldown aura was expected in State");
        }
    }
}
