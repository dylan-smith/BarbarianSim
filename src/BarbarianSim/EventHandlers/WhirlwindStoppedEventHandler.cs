using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WhirlwindStoppedEventHandler : EventHandler<WhirlwindStoppedEvent>
{
    public WhirlwindStoppedEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(WhirlwindStoppedEvent e, SimulationState state)
    {
        e.WhirlwindingAuraExpiredEvent = new AuraExpiredEvent(e.Timestamp, "Whirlwind", Aura.Whirlwinding);
        state.Events.Add(e.WhirlwindingAuraExpiredEvent);
        _log.Verbose($"Created AuraExpiredEvent for Whirlwinding");

        e.ViolentWhirlwindAuraExpiredEvent = new AuraExpiredEvent(e.Timestamp, "Whirlwind", Aura.ViolentWhirlwind);
        state.Events.Add(e.ViolentWhirlwindAuraExpiredEvent);
        _log.Verbose($"Created AuraExpiredEvent for Violent Whirlwind");

        if (state.Events.Any(x => x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.ViolentWhirlwind))
        {
            state.Events.RemoveAll(x => x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.ViolentWhirlwind);
            _log.Verbose($"Removing all future Violent Whirlwind AuraAppliedEvents (at least one)");
        }
    }
}
