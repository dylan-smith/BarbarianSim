using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class DirectDamageEvent : EventInfo
{
    public DirectDamageEvent(double timestamp, double baseDamage, DamageType damageType, DamageSource damageSource, SkillType skillType, double luckyHitChance, Expertise expertise, EnemyState enemy) : base(timestamp)
    {
        BaseDamage = baseDamage;
        DamageType = damageType;
        DamageSource = damageSource;
        SkillType = skillType;
        LuckyHitChance = luckyHitChance;
        Expertise = expertise;
        Enemy = enemy;
    }

    public double BaseDamage { get; init; }
    public DamageType DamageType { get; init; }
    public DamageSource DamageSource { get; init; }
    public SkillType SkillType { get; init; }
    public double LuckyHitChance { get; init; }
    public Expertise Expertise { get; init; }
    public EnemyState Enemy { get; init; }

    public DamageEvent DamageEvent { get; set; }
    public LuckyHitEvent LuckyHitEvent { get; set; }
}
