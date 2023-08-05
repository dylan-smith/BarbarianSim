namespace BarbarianSim.Events;

[Proc("Warbringer")]
public class WarbringerProcEvent : EventInfo
{
    public WarbringerProcEvent(double timestamp) : base(timestamp, null)
    {
    }

    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - Granting 12% Max Life as Fortify";
}
