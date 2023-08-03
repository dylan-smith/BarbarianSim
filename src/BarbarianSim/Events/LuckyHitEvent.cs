using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class LuckyHitEvent : EventInfo
{
    public LuckyHitEvent(double timestamp, SkillType skillType, EnemyState target, GearItem weapon) : base(timestamp)
    {
        SkillType = skillType;
        Target = target;
        Weapon = weapon;
    }

    public SkillType SkillType { get; init; }
    public EnemyState Target { get; init; }
    public GearItem Weapon { get; init; }
}
