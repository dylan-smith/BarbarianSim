namespace BarbarianSim.Events;

public class GutteralYellProcEvent : EventInfo
{
    public GutteralYellProcEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent GutteralYellAuraAppliedEvent { get; set; }
}
