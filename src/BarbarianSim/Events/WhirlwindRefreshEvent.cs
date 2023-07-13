using BarbarianSim.Abilities;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events;

public class WhirlwindRefreshEvent : EventInfo
{
    public WhirlwindRefreshEvent(Whirlwind whirlwind, 
                                 WhirlwindSpinEventFactory whirlwindSpinEventFactory, 
                                 WhirlwindStoppedEventFactory whirlwindStoppedEventFactory, 
                                 double timestamp) : base(timestamp)
    {
        _whirlwind = whirlwind;
        _whirlwindSpinEventFactory = whirlwindSpinEventFactory;
        _whirlwindStoppedEventFactory = whirlwindStoppedEventFactory;
    }

    private readonly Whirlwind _whirlwind;
    private readonly WhirlwindSpinEventFactory _whirlwindSpinEventFactory;
    private readonly WhirlwindStoppedEventFactory _whirlwindStoppedEventFactory;

    public WhirlwindSpinEvent WhirlwindSpinEvent { get; set; }
    public WhirlwindStoppedEvent WhirlwindStoppedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        if (_whirlwind.CanRefresh(state))
        {
            WhirlwindSpinEvent = _whirlwindSpinEventFactory.Create(Timestamp);
            state.Events.Add(WhirlwindSpinEvent);
        }
        else
        {
            WhirlwindStoppedEvent = _whirlwindStoppedEventFactory.Create(Timestamp);
            state.Events.Add(WhirlwindStoppedEvent);
        }
    }
}
