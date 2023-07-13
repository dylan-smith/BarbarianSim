using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class AspectOfTheProtectorProcEventFactory
{
    public AspectOfTheProtectorProcEventFactory(AuraAppliedEventFactory auraAppliedEventFactory, BarrierAppliedEventFactory barrierAppliedEventFactory)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _barrierAppliedEventFactory = barrierAppliedEventFactory;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly BarrierAppliedEventFactory _barrierAppliedEventFactory;

    public AspectOfTheProtectorProcEvent Create(double timestamp, int barrierAmount) => new(_auraAppliedEventFactory, _barrierAppliedEventFactory, timestamp, barrierAmount);
}
