using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class AspectOfTheProtectorCooldownCompletedEvent : EventInfo
{
    public AspectOfTheProtectorCooldownCompletedEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Player.Auras.Remove(Aura.AspectOfTheProtectorCooldown))
        {
            throw new Exception("AspectOfTheProtectorCooldown aura was expected in State");
        }
    }
}
