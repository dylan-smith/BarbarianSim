using BarbarianSim.Abilities;

namespace BarbarianSim.Events;

public class WhirlwindRefreshEvent : EventInfo
{
    public WhirlwindRefreshEvent(double timestamp) : base(timestamp)
    { }

    public WhirlwindStartedEvent WhirlwindStartedEvent { get; set; }
    public WhirlwindStoppedEvent WhirlwindStoppedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        if (Whirlwind.CanRefresh(state))
        {
            WhirlwindStartedEvent = new WhirlwindStartedEvent(Timestamp);
            state.Events.Add(WhirlwindStartedEvent);
        }
        else
        {
            WhirlwindStoppedEvent = new WhirlwindStoppedEvent(Timestamp);
            state.Events.Add(WhirlwindStoppedEvent);
        }
    }
}
