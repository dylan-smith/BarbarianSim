using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WhirlwindStoppedEventHandler : EventHandler<WhirlwindStoppedEvent>
{
    public override void ProcessEvent(WhirlwindStoppedEvent e, SimulationState state)
    {
        e.WhirlwindingAuraExpiredEvent = new AuraExpiredEvent(e.Timestamp, Aura.Whirlwinding);
        state.Events.Add(e.WhirlwindingAuraExpiredEvent);

        e.ViolentWhirlwindAuraExpiredEvent = new AuraExpiredEvent(e.Timestamp, Aura.ViolentWhirlwind);
        state.Events.Add(e.ViolentWhirlwindAuraExpiredEvent);

        state.Events.RemoveAll(x => x is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.ViolentWhirlwind);
    }
}
