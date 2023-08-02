namespace BarbarianSim.Events;

public class WarbringerProcEvent : EventInfo
{
    public WarbringerProcEvent(double timestamp) : base(timestamp)
    {
    }

    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
}
