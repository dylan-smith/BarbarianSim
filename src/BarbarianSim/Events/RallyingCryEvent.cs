namespace BarbarianSim.Events;

public class RallyingCryEvent : EventInfo
{
    public RallyingCryEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent RallyingCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent RallyingCryCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent UnstoppableAuraAppliedEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public double Duration { get; set; }
}
