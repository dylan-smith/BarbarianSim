using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class BarrierAppliedEventFactory
{
    public BarrierAppliedEventFactory(AuraAppliedEventFactory auraAppliedEventFactory, BarrierExpiredEventFactory barrierExpiredEventFactory)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _barrierExpiredEventFactory = barrierExpiredEventFactory;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly BarrierExpiredEventFactory _barrierExpiredEventFactory;

    public BarrierAppliedEvent Create(double timestamp, double barrierAmount, double duration) => new(_auraAppliedEventFactory, _barrierExpiredEventFactory, timestamp, barrierAmount, duration);
}
