using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class WhirlwindSpinEvent : EventInfo
{
    // Rapidly attack surrounding enemies for 17%[x] damage (Fury Cost: 11, Lucky Hit: 20%)
    public WhirlwindSpinEvent(double timestamp) : base(timestamp)
    { }

    public AuraAppliedEvent WhirlwindingAuraAppliedEvent { get; set; }
    public IList<DirectDamageEvent> DirectDamageEvents { get; init; } = new List<DirectDamageEvent>();
    public FurySpentEvent FurySpentEvent { get; set; }
    public WhirlwindRefreshEvent WhirlwindRefreshEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }
    public double BaseDamage { get; set; }
}
