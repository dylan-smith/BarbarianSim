using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class AdditiveDamageBonusCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType, EnemyState target) => Calculate<AdditiveDamageBonusCalculator>(state, damageType, target);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState target)
    {
        var physicalDamage = PhysicalDamageCalculator.Calculate(state, damageType);
        var damageToClose = DamageToCloseCalculator.Calculate(state);
        var damageToInjured = DamageToInjuredCalculator.Calculate(state, target);
        var damageToSlowed = DamageToSlowedCalculator.Calculate(state, target);
        var damageToCrowdControlled = DamageToCrowdControlledCalculator.Calculate(state, target);
        var berserkingDamage = BerserkingDamageCalculator.Calculate(state);

        var bonus = physicalDamage + damageToClose + damageToInjured + damageToSlowed + damageToCrowdControlled + berserkingDamage;

        return 1.0 + (bonus / 100.0);
    }
}
