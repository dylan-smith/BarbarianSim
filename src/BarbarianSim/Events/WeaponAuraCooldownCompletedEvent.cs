using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class WeaponAuraCooldownCompletedEvent : EventInfo
{
    public WeaponAuraCooldownCompletedEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Auras.Remove(Aura.WeaponCooldown))
        {
            throw new Exception("WeaponCooldown aura was expected in State");
        }
    }
}
