using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class FortifyGeneratedEventFactory
{
    public FortifyGeneratedEventFactory(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;
    
    public FortifyGeneratedEvent Create(double timestamp, double amount) => new(_maxLifeCalculator, timestamp, amount);
}
