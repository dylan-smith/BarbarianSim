using BarbarianSim.Events;
using BarbarianSim.Paragon;

namespace BarbarianSim.EventHandlers;

public class WarbringerProcEventHandler : EventHandler<WarbringerProcEvent>
{
    public WarbringerProcEventHandler(Warbringer warbringer, SimLogger log)
    {
        _warbringer = warbringer;
        _log = log;
    }

    private readonly Warbringer _warbringer;
    private readonly SimLogger _log;

    public override void ProcessEvent(WarbringerProcEvent e, SimulationState state)
    {
        var fortify = _warbringer.GetFortifyGenerated(state);
        e.FortifyGeneratedEvent = new FortifyGeneratedEvent(e.Timestamp, "Warbringer", fortify);
        state.Events.Add(e.FortifyGeneratedEvent);
        _log.Verbose($"Created FortifyGeneratedEvent for {fortify:F2} Fortify");
    }
}
