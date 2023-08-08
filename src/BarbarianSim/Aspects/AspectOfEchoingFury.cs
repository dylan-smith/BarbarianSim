using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfEchoingFury : Aspect, IHandlesEvent<ChallengingShoutEvent>, IHandlesEvent<WarCryEvent>, IHandlesEvent<RallyingCryEvent>
{
    // Your Shout skills generate 2-4 Fury per second while active
    public double Fury { get; set; }

    public AspectOfEchoingFury(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            state.Events.Add(new AspectOfEchoingFuryProcEvent(e.Timestamp, e.Duration, Fury));
            _log.Verbose($"Aspect of Echoing Fury created AspectOfEchoingFuryProcEvent for {Fury} per second for {e.Duration} seconds");
        }
    }

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            state.Events.Add(new AspectOfEchoingFuryProcEvent(e.Timestamp, e.Duration, Fury));
            _log.Verbose($"Aspect of Echoing Fury created AspectOfEchoingFuryProcEvent for {Fury} per second for {e.Duration} seconds");
        }
    }

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            state.Events.Add(new AspectOfEchoingFuryProcEvent(e.Timestamp, e.Duration, Fury));
            _log.Verbose($"Aspect of Echoing Fury created AspectOfEchoingFuryProcEvent for {Fury} per second for {e.Duration} seconds");
        }
    }
}
