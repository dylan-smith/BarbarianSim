using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class LuckyHitEventFactory
{
    public LuckyHitEvent Create(double timestamp, SkillType skillType, EnemyState target) => new(timestamp, skillType, target);
}
