namespace BarbarianSim.Events;

public class BarrierAppliedEvent : EventInfo
{
    public double BarrierAmount { get; init; }
    public double Duration { get; init; }
    public Barrier Barrier { get; set; }
    public BarrierExpiredEvent BarrierExpiredEvent { get; set; }
    public AuraAppliedEvent BarrierAuraAppliedEvent { get; set; }

    public BarrierAppliedEvent(double timestamp, double barrierAmount, double duration) : base(timestamp)
    {
        BarrierAmount = barrierAmount;
        Duration = duration;
    }

    public override string ToString() => $"{base.ToString()} - {BarrierAmount} barrier applied for {Duration} seconds";
}
