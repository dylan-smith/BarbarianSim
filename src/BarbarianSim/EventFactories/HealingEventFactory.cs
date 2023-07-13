using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class HealingEventFactory
{
    public HealingEventFactory(HealingReceivedCalculator healingReceivedCalculator, MaxLifeCalculator maxLifeCalculator)
    {
        _healingReceivedCalculator = healingReceivedCalculator;
        _maxLifeCalculator = maxLifeCalculator;
    }

    private readonly HealingReceivedCalculator _healingReceivedCalculator;
    private readonly MaxLifeCalculator _maxLifeCalculator;

    public HealingEvent Create(double timestamp, double baseAmountHealed) => new(_healingReceivedCalculator, _maxLifeCalculator, timestamp, baseAmountHealed);
}
