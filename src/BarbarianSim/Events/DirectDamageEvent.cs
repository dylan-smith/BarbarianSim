using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class DirectDamageEvent : EventInfo
{
    public DirectDamageEvent(double timestamp, string source, double baseDamage, DamageType damageType, DamageSource damageSource, SkillType skillType, double luckyHitChance, GearItem weapon, EnemyState enemy) : base(timestamp, source)
    {
        BaseDamage = baseDamage;
        DamageType = damageType;
        DamageSource = damageSource;
        SkillType = skillType;
        LuckyHitChance = luckyHitChance;
        Weapon = weapon;
        Enemy = enemy;
    }

    public double BaseDamage { get; init; }
    public DamageType DamageType { get; init; }
    public DamageSource DamageSource { get; init; }
    public SkillType SkillType { get; init; }
    public double LuckyHitChance { get; init; }
    public GearItem Weapon { get; init; }
    public EnemyState Enemy { get; init; }

    public DamageEvent DamageEvent { get; set; }
    public LuckyHitEvent LuckyHitEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - {BaseDamage:F2} damage of type {DamageType} (Source: {Source})";
}
