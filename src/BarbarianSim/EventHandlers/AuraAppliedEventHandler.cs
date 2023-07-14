using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class AuraAppliedEventHandler : EventHandler<AuraAppliedEvent>
{
    public override void ProcessEvent(AuraAppliedEvent e, SimulationState state)
    {
        if (e.Target == null)
        {
            state.Player.Auras.Add(e.Aura);
        }
        else
        {
            e.Target.Auras.Add(e.Aura);
        }

        if (e.Duration > 0)
        {
            e.AuraExpiredEvent = new AuraExpiredEvent(e.Timestamp + e.Duration, e.Target, e.Aura);
            state.Events.Add(e.AuraExpiredEvent);
        }
    }
}
