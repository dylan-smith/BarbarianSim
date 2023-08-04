namespace BarbarianSim.Events;

public class GutteralYellProcEvent : EventInfo
{
    public GutteralYellProcEvent(double timestamp) : base(timestamp, null)
    {
    }

    public AuraAppliedEvent GutteralYellAuraAppliedEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - Enemies deal X% less damage for 5 seconds";
}
