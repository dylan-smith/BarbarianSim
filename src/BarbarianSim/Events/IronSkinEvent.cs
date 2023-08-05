namespace BarbarianSim.Events;

[Ability("Iron Skin")]
public class IronSkinEvent : EventInfo
{
    public IronSkinEvent(double timestamp) : base(timestamp, null)
    {
    }

    public AuraAppliedEvent IronSkinAuraAppliedEvent { get; set; }
    public AuraAppliedEvent IronSkinCooldownAuraAppliedEvent { get; set; }
    public BarrierAppliedEvent BarrierAppliedEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - Gain a barrier for 65% of missing life for 5 seconds";
}
