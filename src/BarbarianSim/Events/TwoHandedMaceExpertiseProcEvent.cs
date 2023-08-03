namespace BarbarianSim.Events;

public class TwoHandedMaceExpertiseProcEvent : EventInfo
{
    public TwoHandedMaceExpertiseProcEvent(double timestamp) : base(timestamp)
    {
    }

    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
}
