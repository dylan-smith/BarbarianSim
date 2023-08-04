namespace BarbarianSim.Events;

public class WhirlwindRefreshEvent : EventInfo
{
    public WhirlwindRefreshEvent(double timestamp) : base(timestamp, null)
    { }

    public WhirlwindSpinEvent WhirlwindSpinEvent { get; set; }
    public WhirlwindStoppedEvent WhirlwindStoppedEvent { get; set; }
}
