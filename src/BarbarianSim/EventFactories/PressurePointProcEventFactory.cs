using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class PressurePointProcEventFactory
{
    public PressurePointProcEventFactory(AuraAppliedEventFactory auraAppliedEventFactory) => _auraAppliedEventFactory = auraAppliedEventFactory;

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public PressurePointProcEvent Create(double timestamp, EnemyState target) => new(_auraAppliedEventFactory, timestamp, target);
}
