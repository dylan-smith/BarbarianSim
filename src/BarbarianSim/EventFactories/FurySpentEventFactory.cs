using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class FurySpentEventFactory
{
    public FurySpentEventFactory(FuryCostReductionCalculator furyCostReductionCalculator) => _furyCostReductionCalculator = furyCostReductionCalculator;

    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;

    public FurySpentEvent Create(double timestamp, double furySpent, SkillType skillType) => new(_furyCostReductionCalculator, timestamp, furySpent, skillType);
}
