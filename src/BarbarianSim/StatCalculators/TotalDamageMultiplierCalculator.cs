using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType, EnemyState enemy) => Calculate<TotalDamageMultiplierCalculator>(state, damageType, enemy);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy)
    {
        var damageBonus = AdditiveDamageBonusCalculator.Calculate(state, damageType, enemy);
        damageBonus *= VulnerableDamageBonusCalculator.Calculate(state, enemy);
        damageBonus *= 1 + (StrengthCalculator.Calculate(state) * 0.001);

        if (state.Player.Auras.Contains(Aura.WarCry))
        {
            damageBonus *= WarCry.GetDamageBonus(state);
        }

        return damageBonus;
    }
}
