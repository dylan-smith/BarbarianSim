using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class BleedAppliedEventFactory
{
    public BleedAppliedEventFactory(BleedCompletedEventFactory bleedCompletedEventFactory) => _bleedCompletedEventFactory = bleedCompletedEventFactory;

    private readonly BleedCompletedEventFactory _bleedCompletedEventFactory;
    
    public BleedAppliedEvent Create(double timestamp, double damage, double duration, EnemyState target) => new(_bleedCompletedEventFactory, timestamp, damage, duration, target);
}
