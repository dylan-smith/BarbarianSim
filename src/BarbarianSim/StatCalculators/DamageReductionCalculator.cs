using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class DamageReductionCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState enemy) => Calculate<DamageReductionCalculator>(state, enemy);

    protected override double InstanceCalculate(SimulationState state, EnemyState enemy)
    {
        var damageReduction = 0.9; // Base DR for Barbarians (https://maxroll.gg/d4/getting-started/defenses-for-beginners)
        damageReduction *= state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReduction / 100.0));
        damageReduction *= 1 - (DamageReductionFromBleedingCalculator.Calculate(state, enemy) / 100.0);
        damageReduction *= 1 - (DamageReductionFromCloseCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (DamageReductionWhileFortifiedCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (DamageReductionWhileInjuredCalculator.Calculate(state) / 100.0);

        if (state.Player.Auras.Contains(Aura.ChallengingShout))
        {
            damageReduction *= 1 - (ChallengingShout.GetDamageReduction(state) / 100.0);
        }

        if (state.Player.Auras.Contains(Aura.GutteralYell))
        {
            damageReduction *= 1 - (GutteralYell.GetDamageReduction(state) / 100.0);
        }

        return damageReduction;
    }
}
