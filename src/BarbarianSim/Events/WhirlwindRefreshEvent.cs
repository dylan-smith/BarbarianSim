using BarbarianSim.Abilities;

namespace BarbarianSim.Events;

public class WhirlwindRefreshEvent : EventInfo
{
    public WhirlwindRefreshEvent(double timestamp) : base(timestamp)
    { }

    public WhirlwindSpinEvent WhirlwindSpinEvent { get; set; }
    public WhirlwindStoppedEvent WhirlwindStoppedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        if (Whirlwind.CanRefresh(state))
        {
            WhirlwindSpinEvent = new WhirlwindSpinEvent(Timestamp);
            state.Events.Add(WhirlwindSpinEvent);
        }
        else
        {
            WhirlwindStoppedEvent = new WhirlwindStoppedEvent(Timestamp);
            state.Events.Add(WhirlwindStoppedEvent);
        }
    }
}
