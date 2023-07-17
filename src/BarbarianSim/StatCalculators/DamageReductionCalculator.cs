using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class DamageReductionCalculator
{
    public DamageReductionCalculator(DamageReductionFromBleedingCalculator damageReductionFromBleedingCalculator,
                                     DamageReductionFromCloseCalculator damageReductionFromCloseCalculator,
                                     DamageReductionWhileFortifiedCalculator damageReductionWhileFortifiedCalculator,
                                     DamageReductionWhileInjuredCalculator damageReductionWhileInjuredCalculator,
                                     AggressiveResistance aggressiveResistance,
                                     ChallengingShout challengingShout,
                                     GutteralYell gutteralYell)
    {
        _damageReductionFromBleedingCalculator = damageReductionFromBleedingCalculator;
        _damageReductionFromCloseCalculator = damageReductionFromCloseCalculator;
        _damageReductionWhileFortifiedCalculator = damageReductionWhileFortifiedCalculator;
        _damageReductionWhileInjuredCalculator = damageReductionWhileInjuredCalculator;
        _aggressiveResistance = aggressiveResistance;
        _challengingShout = challengingShout;
        _gutteralYell = gutteralYell;
    }

    private readonly DamageReductionFromBleedingCalculator _damageReductionFromBleedingCalculator;
    private readonly DamageReductionFromCloseCalculator _damageReductionFromCloseCalculator;
    private readonly DamageReductionWhileFortifiedCalculator _damageReductionWhileFortifiedCalculator;
    private readonly DamageReductionWhileInjuredCalculator _damageReductionWhileInjuredCalculator;
    private readonly AggressiveResistance _aggressiveResistance;
    private readonly ChallengingShout _challengingShout;
    private readonly GutteralYell _gutteralYell;

    public virtual double Calculate(SimulationState state, EnemyState enemy)
    {
        var damageReduction = 0.9; // Base DR for Barbarians (https://maxroll.gg/d4/getting-started/defenses-for-beginners)
        damageReduction *= state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReduction / 100.0));
        damageReduction *= 1 - (_damageReductionFromBleedingCalculator.Calculate(state, enemy) / 100.0);
        damageReduction *= 1 - (_damageReductionFromCloseCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (_damageReductionWhileFortifiedCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (_damageReductionWhileInjuredCalculator.Calculate(state) / 100.0);
        damageReduction *= 1 - (_aggressiveResistance.GetDamageReduction(state) / 100.0);

        if (state.Player.Auras.Contains(Aura.ChallengingShout))
        {
            damageReduction *= 1 - (_challengingShout.GetDamageReduction(state) / 100.0);
        }

        if (state.Player.Auras.Contains(Aura.GutteralYell))
        {
            damageReduction *= 1 - (_gutteralYell.GetDamageReduction(state) / 100.0);
        }

        return damageReduction;
    }
}
