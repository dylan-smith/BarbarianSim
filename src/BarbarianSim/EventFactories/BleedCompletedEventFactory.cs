using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class BleedCompletedEventFactory
{
    public BleedCompletedEventFactory(DamageEventFactory damageEventFactory) => _damageEventFactory = damageEventFactory;

    private readonly DamageEventFactory _damageEventFactory;

    public BleedCompletedEvent Create(double timestamp, double damage, EnemyState target) => new(_damageEventFactory, timestamp, damage, target);
}
