namespace BarbarianSim.Events;

[Ability("Challenging Shout")]
public class ChallengingShoutEvent : EventInfo
{
    public ChallengingShoutEvent(double timestamp) : base(timestamp, null)
    {
    }

    public AuraAppliedEvent ChallengingShoutCooldownAuraAppliedEvent { get; set; }
    public double Duration { get; set; }
    public AuraAppliedEvent ChallengingShoutAuraAppliedEvent { get; set; }
    public IList<AuraAppliedEvent> TauntAuraAppliedEvent { get; init; } = new List<AuraAppliedEvent>();

    public override string ToString() => $"{base.ToString()} - Challenging Shout active for {Duration:F2} seconds";
}
