using BarbarianSim.Abilities;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WhirlwindRefreshEventHandler : EventHandler<WhirlwindRefreshEvent>
{
    public override void ProcessEvent(WhirlwindRefreshEvent e, SimulationState state)
    {
        if (Whirlwind.CanRefresh(state))
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
