using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class WhirlwindRefreshEventFactory
{
    public WhirlwindRefreshEventFactory(WhirlwindSpinEventFactory whirlwindSpinEventFactory,
                                        WhirlwindStoppedEventFactory whirlwindStoppedEventFactory,
                                        FuryCostReductionCalculator furyCostReductionCalculator,
                                        AttackSpeedCalculator attackSpeedCalculator,
                                        AuraAppliedEventFactory auraAppliedEventFactory)
    {
        _whirlwindSpinEventFactory = whirlwindSpinEventFactory;
        _whirlwindStoppedEventFactory = whirlwindStoppedEventFactory;
        _furyCostReductionCalculator = furyCostReductionCalculator;
        _attackSpeedCalculator = attackSpeedCalculator;
        _auraAppliedEventFactory = auraAppliedEventFactory;
    }

    private readonly WhirlwindSpinEventFactory _whirlwindSpinEventFactory;
    private readonly WhirlwindStoppedEventFactory _whirlwindStoppedEventFactory;
    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;
    private readonly AttackSpeedCalculator _attackSpeedCalculator;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public WhirlwindRefreshEvent Create(double timestamp) =>
        new(_whirlwindSpinEventFactory, this, _whirlwindStoppedEventFactory, _furyCostReductionCalculator, _attackSpeedCalculator, _auraAppliedEventFactory, timestamp);
}
