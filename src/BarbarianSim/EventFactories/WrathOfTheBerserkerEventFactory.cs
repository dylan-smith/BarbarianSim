using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class WrathOfTheBerserkerEventFactory
{
    public WrathOfTheBerserkerEventFactory(AuraAppliedEventFactory auraAppliedEventFactory) => _auraAppliedEventFactory = auraAppliedEventFactory;

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public WrathOfTheBerserkerEvent Create(double timestamp) => new(_auraAppliedEventFactory, timestamp);
}
