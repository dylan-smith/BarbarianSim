using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class WhirlwindStoppedEvent : EventInfo
{
    public WhirlwindStoppedEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraExpiredEvent WhirlwindingAuraExpiredEvent { get; set; }
    public AuraExpiredEvent ViolentWhirlwindAuraExpiredEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        WhirlwindingAuraExpiredEvent = new AuraExpiredEvent(Timestamp, Aura.Whirlwinding);
        state.Events.Add(WhirlwindingAuraExpiredEvent);

        ViolentWhirlwindAuraExpiredEvent = new AuraExpiredEvent(Timestamp, Aura.ViolentWhirlwind);
        state.Events.Add(ViolentWhirlwindAuraExpiredEvent);

        state.Events.RemoveAll(e => e is AuraAppliedEvent appliedEvent && appliedEvent.Aura == Aura.ViolentWhirlwind);
    }
}
