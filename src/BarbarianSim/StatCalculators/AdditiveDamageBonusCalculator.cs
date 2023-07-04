namespace BarbarianSim.StatCalculators;

public class AdditiveDamageBonusCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType) => Calculate<AdditiveDamageBonusCalculator>(state, damageType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType)
    {
        var physicalDamage = PhysicalDamageCalculator.Calculate(state, damageType);
        var damageToClose = DamageToCloseCalculator.Calculate(state);
        var damageToInjured = DamageToInjuredCalculator.Calculate(state);
        var damageToSlowed = DamageToSlowedCalculator.Calculate(state);
        var damageToCrowdControlled = DamageToCrowdControlledCalculator.Calculate(state);

        var bonus = physicalDamage + damageToClose + damageToInjured + damageToSlowed + damageToCrowdControlled;

        return 1.0 + (bonus / 100.0);
    }
}
