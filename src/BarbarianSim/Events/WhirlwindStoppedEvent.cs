namespace BarbarianSim.Events;

public class WhirlwindStoppedEvent : EventInfo
{
    public WhirlwindStoppedEvent(double timestamp) : base(timestamp, null)
    {
    }

    public AuraExpiredEvent WhirlwindingAuraExpiredEvent { get; set; }
    public AuraExpiredEvent ViolentWhirlwindAuraExpiredEvent { get; set; }
}
