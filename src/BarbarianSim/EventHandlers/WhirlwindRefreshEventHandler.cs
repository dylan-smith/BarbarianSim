using BarbarianSim.Abilities;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WhirlwindRefreshEventHandler : EventHandler<WhirlwindRefreshEvent>
{
    public WhirlwindRefreshEventHandler(Whirlwind whirlwind) => _whirlwind = whirlwind;

    private readonly Whirlwind _whirlwind;

    public override void ProcessEvent(WhirlwindRefreshEvent e, SimulationState state)
    {
        if (_whirlwind.CanRefresh(state))
        {
            e.WhirlwindSpinEvent = new WhirlwindSpinEvent(e.Timestamp);
            state.Events.Add(e.WhirlwindSpinEvent);
        }
        else
        {
            e.WhirlwindStoppedEvent = new WhirlwindStoppedEvent(e.Timestamp);
            state.Events.Add(e.WhirlwindStoppedEvent);
        }
    }
}
