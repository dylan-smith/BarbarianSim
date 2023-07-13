using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class AuraAppliedEventFactory
{
    public AuraAppliedEventFactory(AuraExpiredEventFactory auraExpiredEventFactory) => _auraExpiredEventFactory = auraExpiredEventFactory;

    private readonly AuraExpiredEventFactory _auraExpiredEventFactory;

    public AuraAppliedEvent Create(double timestamp, double duration, Aura aura) => new(_auraExpiredEventFactory, timestamp, duration, aura);

    public AuraAppliedEvent Create(double timestamp, double duration, Aura aura, EnemyState target) => new(_auraExpiredEventFactory, timestamp, duration, aura, target);
}
