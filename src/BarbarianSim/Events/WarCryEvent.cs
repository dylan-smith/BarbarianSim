namespace BarbarianSim.Events;

public class WarCryEvent : EventInfo
{
    public WarCryEvent(double timestamp) : base(timestamp, null)
    {
    }

    public AuraAppliedEvent WarCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WarCryCooldownAuraAppliedEvent { get; set; }
    public double Duration { get; set; }

    public override string ToString() => $"{base.ToString()} - Increases damage by X% for {Duration:F2} seconds";
}
