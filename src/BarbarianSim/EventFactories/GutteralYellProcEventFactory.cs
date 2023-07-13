using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class GutteralYellProcEventFactory
{
    public GutteralYellProcEventFactory(AuraAppliedEventFactory auraAppliedEventFactory) => _auraAppliedEventFactory = auraAppliedEventFactory;

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public GutteralYellProcEvent Create(double timestamp) => new(_auraAppliedEventFactory, timestamp);
}
