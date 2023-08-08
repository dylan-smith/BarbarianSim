using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class AuraExpiredEventHandler : EventHandler<AuraExpiredEvent>
{
    public AuraExpiredEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(AuraExpiredEvent e, SimulationState state)
    {
        if (e.Target == null)
        {
            // if there are other events it means there's an Aura been applied with a later expiration time
            if (state.Events.Any(x => x is AuraExpiredEvent expiredEvent && expiredEvent.Aura == e.Aura))
            {
                _log.Verbose($"Doing nothing because a later AuraExpiredEvent for {e.Aura} exists");
            }
            else
            {
                state.Player.Auras.Remove(e.Aura);
            }
        }
        else
        {
            if (state.Events.Any(x => x is AuraExpiredEvent expiredEvent && expiredEvent.Target == e.Target && expiredEvent.Aura == e.Aura))
            {
                _log.Verbose($"Doing nothing because a later AuraExpiredEvent for {e.Aura} on Enemy #{e.Target.Id} exists");
            }
            else
            {
                e.Target.Auras.Remove(e.Aura);
            }
        }
    }
}
