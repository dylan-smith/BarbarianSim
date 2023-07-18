namespace BarbarianSim.Events;

public class WarCryEvent : EventInfo
{
    public WarCryEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent WarCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WarCryCooldownAuraAppliedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public double Duration { get; set; }
}
