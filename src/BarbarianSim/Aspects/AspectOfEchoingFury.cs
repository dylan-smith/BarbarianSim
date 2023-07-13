using BarbarianSim.Config;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfEchoingFury : Aspect
{
    // Your Shout skills generate 2-4 Fury per second while active
    public double Fury { get; init; }

    public AspectOfEchoingFury(AspectOfEchoingFuryProcEventFactory aspectOfEchoingFuryProcEventFactory, int fury)
    {
        _aspectOfEchoingFuryProcEventFactory = aspectOfEchoingFuryProcEventFactory;
        Fury = fury;
    }

    private readonly AspectOfEchoingFuryProcEventFactory _aspectOfEchoingFuryProcEventFactory;

    public void ProcessEvent(ChallengingShoutEvent challengingShoutEvent, SimulationState state)
    {
        state.Events.Add(_aspectOfEchoingFuryProcEventFactory.Create(challengingShoutEvent.Timestamp, challengingShoutEvent.Duration, Fury));
    }

    public void ProcessEvent(WarCryEvent warCryEvent, SimulationState state)
    {
        state.Events.Add(_aspectOfEchoingFuryProcEventFactory.Create(warCryEvent.Timestamp, warCryEvent.Duration, Fury));
    }

    public void ProcessEvent(RallyingCryEvent rallyingCryEvent, SimulationState state)
    {
        state.Events.Add(_aspectOfEchoingFuryProcEventFactory.Create(rallyingCryEvent.Timestamp, rallyingCryEvent.Duration, Fury));
    }
}
