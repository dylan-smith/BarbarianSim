using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class AspectOfEchoingFuryFactory
{
    public AspectOfEchoingFuryFactory(AspectOfEchoingFuryProcEventFactory aspectOfEchoingFuryProcEventFactory) => _aspectOfEchoingFuryProcEventFactory = aspectOfEchoingFuryProcEventFactory;

    private readonly AspectOfEchoingFuryProcEventFactory _aspectOfEchoingFuryProcEventFactory;

    public AspectOfEchoingFury Create(int fury) => new(_aspectOfEchoingFuryProcEventFactory, fury);
}
