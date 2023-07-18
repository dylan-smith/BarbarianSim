using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfEchoingFury : Aspect, IHandlesEvent<ChallengingShoutEvent>, IHandlesEvent<WarCryEvent>, IHandlesEvent<RallyingCryEvent>
{
    // Your Shout skills generate 2-4 Fury per second while active
    public double Fury { get; set; }

    public void ProcessEvent(ChallengingShoutEvent e, SimulationState state)
    {
        state.Events.Add(new AspectOfEchoingFuryProcEvent(e.Timestamp, e.Duration, Fury));
    }

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        state.Events.Add(new AspectOfEchoingFuryProcEvent(e.Timestamp, e.Duration, Fury));
    }

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        state.Events.Add(new AspectOfEchoingFuryProcEvent(e.Timestamp, e.Duration, Fury));
    }
}
