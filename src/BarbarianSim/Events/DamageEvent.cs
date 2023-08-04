using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class DamageEvent : EventInfo
{
    public double Damage { get; init; }
    public DamageType DamageType { get; init; }
    public DamageSource DamageSource { get; init; }
    public SkillType SkillType { get; init; }
    public EnemyState Target { get; init; }

    public DamageEvent(double timestamp, string source, double damage, DamageType damageType, DamageSource damageSource, SkillType skillType, EnemyState target) : base(timestamp, source)
    {
        Damage = damage;
        DamageType = damageType;
        DamageSource = damageSource;
        SkillType = skillType;
        Target = target;
    }

    public override string ToString() => $"{base.ToString()} - {Damage:F2} dealt to Enemy #{Target.Id} ({DamageType}) (Source: {Source})";
}
