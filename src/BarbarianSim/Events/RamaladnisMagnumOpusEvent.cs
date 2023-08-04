namespace BarbarianSim.Events;

public class RamaladnisMagnumOpusEvent : EventInfo
{
    public RamaladnisMagnumOpusEvent(double timestamp) : base(timestamp, null)
    {
    }

    public FurySpentEvent FurySpentEvent { get; set; }
    public RamaladnisMagnumOpusEvent NextRamaladnisMagnumOpusEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - Losing 2 Fury every second";
}
