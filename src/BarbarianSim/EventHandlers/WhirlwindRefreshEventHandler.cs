using BarbarianSim.Abilities;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WhirlwindRefreshEventHandler : EventHandler<WhirlwindRefreshEvent>
{
    public WhirlwindRefreshEventHandler(Whirlwind whirlwind, SimLogger log)
    {
        _whirlwind = whirlwind;
        _log = log;
    }

    private readonly Whirlwind _whirlwind;
    private readonly SimLogger _log;

    public override void ProcessEvent(WhirlwindRefreshEvent e, SimulationState state)
    {
        if (_whirlwind.CanRefresh(state))
        {
            e.WhirlwindSpinEvent = new WhirlwindSpinEvent(e.Timestamp);
            state.Events.Add(e.WhirlwindSpinEvent);
            _log.Verbose($"Created WhirlwindSpinEvent");
        }
        else
        {
            e.WhirlwindStoppedEvent = new WhirlwindStoppedEvent(e.Timestamp);
            state.Events.Add(e.WhirlwindStoppedEvent);
            _log.Verbose($"Created WhirlwindStoppedEvent");
        }
    }
}
