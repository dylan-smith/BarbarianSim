namespace BarbarianSim.Events;

public class RallyingCryEvent : EventInfo
{
    public RallyingCryEvent(double timestamp) : base(timestamp, null)
    {
    }

    public AuraAppliedEvent RallyingCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent RallyingCryCooldownAuraAppliedEvent { get; set; }
    public double Duration { get; set; }

    public override string ToString() => $"{base.ToString()} - Increasing Movement Speed and Resource Generation for 6 seconds";
}
