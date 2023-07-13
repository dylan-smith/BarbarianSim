using BarbarianSim.Abilities;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class LungingStrikeEventFactory
{
    public LungingStrikeEventFactory(FuryGeneratedEventFactory furyGeneratedEventFactory,
                                     LungingStrike lungingStrike,
                                     DirectDamageEventFactory directDamageEventFactory,
                                     AttackSpeedCalculator attackSpeedCalculator,
                                     AuraAppliedEventFactory auraAppliedEventFactory)
    {
        _furyGeneratedEventFactory = furyGeneratedEventFactory;
        _lungingStrike = lungingStrike;
        _directDamageEventFactory = directDamageEventFactory;
        _attackSpeedCalculator = attackSpeedCalculator;
        _auraAppliedEventFactory = auraAppliedEventFactory;
    }

    private readonly FuryGeneratedEventFactory _furyGeneratedEventFactory;
    private readonly LungingStrike _lungingStrike;
    private readonly DirectDamageEventFactory _directDamageEventFactory;
    private readonly AttackSpeedCalculator _attackSpeedCalculator;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    
    public LungingStrikeEvent Create(double timestamp, EnemyState target) => 
        new(_furyGeneratedEventFactory, _lungingStrike, _directDamageEventFactory, _attackSpeedCalculator, _auraAppliedEventFactory, timestamp, target);
}
