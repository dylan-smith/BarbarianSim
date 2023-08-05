namespace BarbarianSim.Events;

[Ability("Wrath of the Berserker")]
public class WrathOfTheBerserkerEvent : EventInfo
{
    public WrathOfTheBerserkerEvent(double timestamp) : base(timestamp, null)
    {
    }

    public AuraAppliedEvent WrathOfTheBerserkerAuraAppliedEvent { get; set; }
    public AuraAppliedEvent UnstoppableAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WrathOfTheBerserkerCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }
}
