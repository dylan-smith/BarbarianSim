namespace BarbarianSim.Events;

public class BarrierExpiredEvent : EventInfo
{
    public Barrier Barrier { get; init; }

    public BarrierExpiredEvent(double timestamp, string source, Barrier barrier) : base(timestamp, source) => Barrier = barrier;
}
