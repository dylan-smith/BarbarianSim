using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class FuryGeneratedEventFactory
{
    public FuryGeneratedEventFactory(ResourceGenerationCalculator resourceGenerationCalculator, MaxFuryCalculator maxFuryCalculator)
    {
        _resourceGenerationCalculator = resourceGenerationCalculator;
        _maxFuryCalculator = maxFuryCalculator;
    }

    private readonly ResourceGenerationCalculator _resourceGenerationCalculator;
    private readonly MaxFuryCalculator _maxFuryCalculator;

    public FuryGeneratedEvent Create(double timestamp, double fury) => new(_resourceGenerationCalculator, _maxFuryCalculator, timestamp, fury);
}
