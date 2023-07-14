using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class DamageEventHandler : EventHandler<DamageEvent>
{
    public override void ProcessEvent(DamageEvent e, SimulationState state) => e.Target.Life -= (int)e.Damage;
}
