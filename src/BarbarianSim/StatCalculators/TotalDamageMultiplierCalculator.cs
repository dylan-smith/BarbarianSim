namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType) => Calculate<TotalDamageMultiplierCalculator>(state, damageType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType)
    {
        var damageBonus = AdditiveDamageBonusCalculator.Calculate(state, damageType);
        damageBonus *= VulnerableDamageBonusCalculator.Calculate(state);
        damageBonus *= 1 + (StrengthCalculator.Calculate(state) * 0.001);

        return damageBonus;
    }
}
