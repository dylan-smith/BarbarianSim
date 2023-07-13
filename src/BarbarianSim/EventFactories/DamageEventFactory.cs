using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class DamageEventFactory
{
    public DamageEvent Create(double timestamp, double damage, DamageType damageType, DamageSource damageSource, SkillType skillType, EnemyState target) => new(timestamp, damage, damageType, damageSource, skillType, target);
}
