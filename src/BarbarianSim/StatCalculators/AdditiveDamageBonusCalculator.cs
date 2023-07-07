using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class AdditiveDamageBonusCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType, EnemyState enemy) => Calculate<AdditiveDamageBonusCalculator>(state, damageType, enemy);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy)
    {
        var physicalDamage = PhysicalDamageCalculator.Calculate(state, damageType);
        var damageToClose = DamageToCloseCalculator.Calculate(state);
        var damageToInjured = DamageToInjuredCalculator.Calculate(state, enemy);
        var damageToSlowed = DamageToSlowedCalculator.Calculate(state, enemy);
        var damageToCrowdControlled = DamageToCrowdControlledCalculator.Calculate(state, enemy);
        var berserkingDamage = BerserkingDamageCalculator.Calculate(state);

        var bonus = physicalDamage + damageToClose + damageToInjured + damageToSlowed + damageToCrowdControlled + berserkingDamage;

        return 1.0 + (bonus / 100.0);
    }
}
