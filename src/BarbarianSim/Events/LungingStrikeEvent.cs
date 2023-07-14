using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class LungingStrikeEvent : EventInfo
{
    public LungingStrikeEvent(double timestamp, EnemyState target) : base(timestamp) => Target = target;

    public EnemyState Target { get; init; }
    public DirectDamageEvent DirectDamageEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }
    public double BaseDamage { get; set; }
}
