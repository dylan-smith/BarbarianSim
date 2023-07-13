using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class AuraExpiredEventFactory
{
    public AuraExpiredEvent Create(double timestamp, Aura aura) => new(timestamp, aura);

    public AuraExpiredEvent Create(double timestamp, EnemyState target, Aura aura) => new(timestamp, target, aura);
}
