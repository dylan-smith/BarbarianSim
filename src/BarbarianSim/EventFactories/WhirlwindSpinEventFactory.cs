using BarbarianSim.Abilities;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class WhirlwindSpinEventFactory
{
    public WhirlwindSpinEventFactory(Whirlwind whirlwind,
                                     AuraAppliedEventFactory auraAppliedEventFactory,
                                     FurySpentEventFactory furySpentEventFactory,
                                     DirectDamageEventFactory directDamageEventFactory,
                                     WhirlwindRefreshEventFactory whirlwindRefreshEventFactory,
                                     AttackSpeedCalculator attackSpeedCalculator)
    {
        _whirlwind = whirlwind;
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _furySpentEventFactory = furySpentEventFactory;
        _directDamageEventFactory = directDamageEventFactory;
        _whirlwindRefreshEventFactory = whirlwindRefreshEventFactory;
        _attackSpeedCalculator = attackSpeedCalculator;
    }

    private readonly Whirlwind _whirlwind;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly FurySpentEventFactory _furySpentEventFactory;
    private readonly DirectDamageEventFactory _directDamageEventFactory;
    private readonly WhirlwindRefreshEventFactory _whirlwindRefreshEventFactory;
    private readonly AttackSpeedCalculator _attackSpeedCalculator;

    public WhirlwindSpinEvent Create(double timestamp) => new(_whirlwind, _auraAppliedEventFactory, _furySpentEventFactory, _directDamageEventFactory, _whirlwindRefreshEventFactory, _attackSpeedCalculator, timestamp);
}
