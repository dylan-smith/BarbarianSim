using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class GhostwalkerAspectFactory
{
    public GhostwalkerAspectFactory(AuraAppliedEventFactory auraAppliedEventFactory) => _auraAppliedEventFactory = auraAppliedEventFactory;

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public GhostwalkerAspect Create(int speed) => new(_auraAppliedEventFactory, speed);
}
