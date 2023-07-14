using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class DamageReductionCalculator
{
    public DamageReductionCalculator(DamageReductionFromBleedingCalculator damageReductionFromBleedingCalculator,
                                     DamageReductionFromCloseCalculator damageReductionFromCloseCalculator,
                                     DamageReductionWhileFortifiedCalculator damageReductionWhileFortifiedCalculator,
                                     DamageReductionWhileInjuredCalculator damageReductionWhileInjuredCalculator)
    {
        _damageReductionFromBleedingCalculator = damageReductionFromBleedingCalculator;
        _damageReductionFromCloseCalculator = damageReductionFromCloseCalculator;
        _damageReductionWhileFortifiedCalculator = damageReductionWhileFortifiedCalculator;
        _damageReductionWhileInjuredCalculator = damageReductionWhileInjuredCalculator;
    }

    private readonly DamageReductionFromBleedingCalculator _damageReductionFromBleedingCalculator;
    private readonly DamageReductionFromCloseCalculator _damageReductionFromCloseCalculator;
    private readonly DamageReductionWhileFortifiedCalculator _damageReductionWhileFortifiedCalculator;
    private readonly DamageReductionWhileInjuredCalculator _damageReductionWhileInjuredCalculator;
    
    public double Calculate(SimulationState state, EnemyState enemy)
    {
        var damageReduction = 0.9; // Base DR for Barbarians (https://maxroll.gg/d4/getting-started/defenses-for-beginners)
        damageReduction *= state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReduction / 100.0));
        damageReduction *= 1 - (_damageReductionFromBleedingCalculator.Calculate(state, enemy) / 100.0);
        damageReduction *= 1 - (_damageReductionFromCloseCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (_damageReductionWhileFortifiedCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (_damageReductionWhileInjuredCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (AggressiveResistance.GetDamageReduction(state) / 100.0);

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
