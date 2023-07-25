namespace BarbarianSim.Events;

public class RamaladnisMagnumOpusEvent : EventInfo
{
    public RamaladnisMagnumOpusEvent(double timestamp) : base(timestamp)
    {
    }

    public FurySpentEvent FurySpentEvent { get; set; }
    public RamaladnisMagnumOpusEvent NextRamaladnisMagnumOpusEvent { get; set; }
}
