using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class AdditiveDamageBonusCalculator
{
    public AdditiveDamageBonusCalculator(PhysicalDamageCalculator physicalDamageCalculator,
                                         DamageToCloseCalculator damageToCloseCalculator,
                                         DamageToInjuredCalculator damageToInjuredCalculator,
                                         DamageToSlowedCalculator damageToSlowedCalculator,
                                         DamageToCrowdControlledCalculator damageToCrowdControlledCalculator,
                                         BerserkingDamageCalculator berserkingDamageCalculator)
    {
        _physicalDamageCalculator = physicalDamageCalculator;
        _damageToCloseCalculator = damageToCloseCalculator;
        _damageToInjuredCalculator = damageToInjuredCalculator;
        _damageToSlowedCalculator = damageToSlowedCalculator;
        _damageToCrowdControlledCalculator = damageToCrowdControlledCalculator;
        _berserkingDamageCalculator = berserkingDamageCalculator;
    }

    private readonly PhysicalDamageCalculator _physicalDamageCalculator;
    private readonly DamageToCloseCalculator _damageToCloseCalculator;
    private readonly DamageToInjuredCalculator _damageToInjuredCalculator;
    private readonly DamageToSlowedCalculator _damageToSlowedCalculator;
    private readonly DamageToCrowdControlledCalculator _damageToCrowdControlledCalculator;
    private readonly BerserkingDamageCalculator _berserkingDamageCalculator;

    public virtual double Calculate(SimulationState state, DamageType damageType, EnemyState enemy)
    {
        var physicalDamage = _physicalDamageCalculator.Calculate(state, damageType);
        var damageToClose = _damageToCloseCalculator.Calculate(state);
        var damageToInjured = _damageToInjuredCalculator.Calculate(state, enemy);
        var damageToSlowed = _damageToSlowedCalculator.Calculate(state, enemy);
        var damageToCrowdControlled = _damageToCrowdControlledCalculator.Calculate(state, enemy);
        var berserkingDamage = _berserkingDamageCalculator.Calculate(state);

        var bonus = physicalDamage + damageToClose + damageToInjured + damageToSlowed + damageToCrowdControlled + berserkingDamage;

        return 1.0 + (bonus / 100.0);
    }
}
