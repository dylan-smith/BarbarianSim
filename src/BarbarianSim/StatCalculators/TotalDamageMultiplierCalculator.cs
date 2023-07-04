namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType) => Calculate<TotalDamageMultiplierCalculator>(state, damageType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType)
    {
        return AdditiveDamageBonusCalculator.Calculate(state, damageType) * VulnerableDamageBonusCalculator.Calculate(state);
    }
}
