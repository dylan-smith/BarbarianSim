using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class AspectOfTheProtectorFactory
{
    public AspectOfTheProtectorFactory(AspectOfTheProtectorProcEventFactory aspectOfTheProtectorProcEventFactory) => _aspectOfTheProtectorProcEventFactory = aspectOfTheProtectorProcEventFactory;

    private readonly AspectOfTheProtectorProcEventFactory _aspectOfTheProtectorProcEventFactory;

    public AspectOfTheProtector Create(int barrierAmount) => new(_aspectOfTheProtectorProcEventFactory, barrierAmount);
}
