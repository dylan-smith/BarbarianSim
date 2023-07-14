using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class AspectOfEchoingFuryProcEventFactory
{
    public AspectOfEchoingFuryProcEventFactory(FuryGeneratedEventFactory furyGeneratedEventFactory) => _furyGeneratedEventFactory = furyGeneratedEventFactory;

    private readonly FuryGeneratedEventFactory _furyGeneratedEventFactory;

    public AspectOfEchoingFuryProcEvent Create(double timestamp, double duration, double fury) => new(_furyGeneratedEventFactory, timestamp, duration, fury);
}
