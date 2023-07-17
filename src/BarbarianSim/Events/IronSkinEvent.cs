namespace BarbarianSim.Events;

public class IronSkinEvent : EventInfo
{
    public IronSkinEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent IronSkinAuraAppliedEvent { get; set; }
    public AuraAppliedEvent IronSkinCooldownAuraAppliedEvent { get; set; }
    public BarrierAppliedEvent BarrierAppliedEvent { get; set; }
    public IList<HealingEvent> HealingEvents { get; init; } = new List<HealingEvent>();
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
}
