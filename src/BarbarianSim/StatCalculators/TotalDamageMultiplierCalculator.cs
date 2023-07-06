using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType, EnemyState target) => Calculate<TotalDamageMultiplierCalculator>(state, damageType, target);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState target)
    {
        var damageBonus = AdditiveDamageBonusCalculator.Calculate(state, damageType, target);
        damageBonus *= VulnerableDamageBonusCalculator.Calculate(state, target);
        damageBonus *= 1 + (StrengthCalculator.Calculate(state) * 0.001);

        return damageBonus;
    }
}
