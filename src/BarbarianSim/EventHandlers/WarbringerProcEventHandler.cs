using BarbarianSim.Events;
using BarbarianSim.Paragon;

namespace BarbarianSim.EventHandlers;

public class WarbringerProcEventHandler : EventHandler<WarbringerProcEvent>
{
    public WarbringerProcEventHandler(Warbringer warbringer) => _warbringer = warbringer;

    private readonly Warbringer _warbringer;

    public override void ProcessEvent(WarbringerProcEvent e, SimulationState state)
    {
        e.FortifyGeneratedEvent = new FortifyGeneratedEvent(e.Timestamp, "Warbringer", _warbringer.GetFortifyGenerated(state));
        state.Events.Add(e.FortifyGeneratedEvent);
    }
}
