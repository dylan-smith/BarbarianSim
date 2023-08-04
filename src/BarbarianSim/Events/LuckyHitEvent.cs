using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class LuckyHitEvent : EventInfo
{
    public LuckyHitEvent(double timestamp, string source, SkillType skillType, EnemyState target, GearItem weapon) : base(timestamp, source)
    {
        SkillType = skillType;
        Target = target;
        Weapon = weapon;
    }

    public SkillType SkillType { get; init; }
    public EnemyState Target { get; init; }
    public GearItem Weapon { get; init; }

    public override string ToString() => $"{base.ToString()} - (Source: {Source})";
}
