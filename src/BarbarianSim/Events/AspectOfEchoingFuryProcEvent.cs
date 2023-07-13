using BarbarianSim.EventFactories;

namespace BarbarianSim.Events;

public class AspectOfEchoingFuryProcEvent : EventInfo
{
    public AspectOfEchoingFuryProcEvent(FuryGeneratedEventFactory furyGeneratedEventFactory, double timestamp, double duration, double fury) : base(timestamp)
    {
        _furyGeneratedEventFactory = furyGeneratedEventFactory;
        Duration = duration;
        Fury = fury;
    }

    private readonly FuryGeneratedEventFactory _furyGeneratedEventFactory;

    public double Duration { get; init; }
    public double Fury { get; init; }
    public IList<FuryGeneratedEvent> FuryGeneratedEvents { get; init; } = new List<FuryGeneratedEvent>();

    public override void ProcessEvent(SimulationState state)
    {
        for (var i = 0; i < Math.Floor(Duration); i++)
        {
            var furyEvent = _furyGeneratedEventFactory.Create(Timestamp + i + 1, Fury);
            FuryGeneratedEvents.Add(furyEvent);
            state.Events.Add(furyEvent);
        }
    }
}
