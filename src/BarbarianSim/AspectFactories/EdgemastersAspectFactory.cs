using BarbarianSim.Aspects;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class EdgemastersAspectFactory
{
    public EdgemastersAspectFactory(MaxFuryCalculator maxFuryCalculator) => _maxFuryCalculator = maxFuryCalculator;

    private readonly MaxFuryCalculator _maxFuryCalculator;

    public EdgemastersAspect Create(int damage) => new(_maxFuryCalculator, damage);
}
