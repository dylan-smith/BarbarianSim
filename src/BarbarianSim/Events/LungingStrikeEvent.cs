namespace BarbarianSim.Events;

[Ability("Lunging Strike")]
public class LungingStrikeEvent : EventInfo
{
    public LungingStrikeEvent(double timestamp, EnemyState target) : base(timestamp, null) => Target = target;

    public EnemyState Target { get; init; }
    public DirectDamageEvent DirectDamageEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }
    public double BaseDamage { get; set; }

    public override string ToString() => $"{base.ToString()} - {BaseDamage:F2} base damage to Enemy #{Target.Id}";
}
