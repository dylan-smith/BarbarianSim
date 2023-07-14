using BarbarianSim.Abilities;

namespace BarbarianSim.Events;

public class WhirlwindRefreshEvent : EventInfo
{
    public WhirlwindRefreshEvent(double timestamp) : base(timestamp)
    { }

    public WhirlwindSpinEvent WhirlwindSpinEvent { get; set; }
    public WhirlwindStoppedEvent WhirlwindStoppedEvent { get; set; }
}
