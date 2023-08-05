namespace BarbarianSim.Events;

[Proc("Aspect of Echoing Fury")]
public class AspectOfEchoingFuryProcEvent : EventInfo
{
    public AspectOfEchoingFuryProcEvent(double timestamp, double duration, double fury) : base(timestamp, null)
    {
        Duration = duration;
        Fury = fury;
    }

    public double Duration { get; init; }
    public double Fury { get; init; }
    public IList<FuryGeneratedEvent> FuryGeneratedEvents { get; init; } = new List<FuryGeneratedEvent>();

    public override string ToString() => $"{base.ToString()} - {Fury:F2} fury generated per second for the next {Duration:F2} seconds";
}
