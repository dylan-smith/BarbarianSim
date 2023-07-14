using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfEchoingFury : Aspect
{
    // Your Shout skills generate 2-4 Fury per second while active
    public double Fury { get; set; }

    public void ProcessEvent(ChallengingShoutEvent challengingShoutEvent, SimulationState state)
    {
        state.Events.Add(new AspectOfEchoingFuryProcEvent(challengingShoutEvent.Timestamp, challengingShoutEvent.Duration, Fury));
    }

    public void ProcessEvent(WarCryEvent warCryEvent, SimulationState state)
    {
        state.Events.Add(new AspectOfEchoingFuryProcEvent(warCryEvent.Timestamp, warCryEvent.Duration, Fury));
    }

    public void ProcessEvent(RallyingCryEvent rallyingCryEvent, SimulationState state)
    {
        state.Events.Add(new AspectOfEchoingFuryProcEvent(rallyingCryEvent.Timestamp, rallyingCryEvent.Duration, Fury));
    }
}
