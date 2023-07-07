using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class ChallengingShoutCooldownCompletedEvent : EventInfo
{
    public ChallengingShoutCooldownCompletedEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Player.Auras.Remove(Aura.ChallengingShoutCooldown))
        {
            throw new Exception("ChallengingShoutCooldown aura was expected in State");
        }
    }
}
