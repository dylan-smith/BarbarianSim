using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class WhirlwindSpinEventFactory
{
    public WhirlwindSpinEventFactory(AuraAppliedEventFactory auraAppliedEventFactory,
                                     FurySpentEventFactory furySpentEventFactory,
                                     DirectDamageEventFactory directDamageEventFactory)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _furySpentEventFactory = furySpentEventFactory;
        _directDamageEventFactory = directDamageEventFactory;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly FurySpentEventFactory _furySpentEventFactory;
    private readonly DirectDamageEventFactory _directDamageEventFactory;

    public WhirlwindSpinEvent Create(double timestamp) => new(_auraAppliedEventFactory, _furySpentEventFactory, _directDamageEventFactory, timestamp);
}
