namespace BarbarianSim.Events;

public class BarrierAppliedEvent : EventInfo
{
    public double BarrierAmount { get; init; }
    public double Duration { get; init; }
    public Barrier Barrier { get; set; }
    public BarrierExpiredEvent BarrierExpiredEvent { get; set; }

    public BarrierAppliedEvent(double timestamp, string source, double barrierAmount, double duration) : base(timestamp, source)
    {
        BarrierAmount = barrierAmount;
        Duration = duration;
    }

    public override string ToString() => $"{base.ToString()} - {BarrierAmount:F2} barrier applied for {Duration:F2} seconds (Source: {Source})";
}
