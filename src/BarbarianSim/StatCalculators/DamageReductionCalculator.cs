using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Paragon;
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
                                     GutteralYell gutteralYell,
                                     AspectOfTheIronWarrior aspectOfTheIronWarrior,
                                     IronBloodAspect ironBloodAspect,
                                     Undaunted undaunted,
                                     SimLogger log)
    {
        _damageReductionFromBleedingCalculator = damageReductionFromBleedingCalculator;
        _damageReductionFromCloseCalculator = damageReductionFromCloseCalculator;
        _damageReductionWhileFortifiedCalculator = damageReductionWhileFortifiedCalculator;
        _damageReductionWhileInjuredCalculator = damageReductionWhileInjuredCalculator;
        _aggressiveResistance = aggressiveResistance;
        _challengingShout = challengingShout;
        _gutteralYell = gutteralYell;
        _aspectOfTheIronWarrior = aspectOfTheIronWarrior;
        _ironBloodAspect = ironBloodAspect;
        _undaunted = undaunted;
        _log = log;
    }

    private readonly DamageReductionFromBleedingCalculator _damageReductionFromBleedingCalculator;
    private readonly DamageReductionFromCloseCalculator _damageReductionFromCloseCalculator;
    private readonly DamageReductionWhileFortifiedCalculator _damageReductionWhileFortifiedCalculator;
    private readonly DamageReductionWhileInjuredCalculator _damageReductionWhileInjuredCalculator;
    private readonly AggressiveResistance _aggressiveResistance;
    private readonly ChallengingShout _challengingShout;
    private readonly GutteralYell _gutteralYell;
    private readonly AspectOfTheIronWarrior _aspectOfTheIronWarrior;
    private readonly IronBloodAspect _ironBloodAspect;
    private readonly Undaunted _undaunted;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, EnemyState enemy)
    {
        var damageReduction = 0.9; // Base DR for Barbarians (https://maxroll.gg/d4/getting-started/defenses-for-beginners)
        _log.Verbose("Base Damage Reduction = 0.90x");

        var damageReductionFromConfig = state.Config.GetStatTotalMultiplied(g => 1 - (g.DamageReduction / 100.0));
        if (damageReductionFromConfig != 1)
        {
            _log.Verbose($"Damage Reduction from Config = {damageReductionFromConfig:F2}x");
        }

        damageReduction *= damageReductionFromConfig;
        damageReduction *= _damageReductionFromBleedingCalculator.Calculate(state, enemy);
        damageReduction *= _damageReductionFromCloseCalculator.Calculate(state);
        damageReduction *= _damageReductionWhileFortifiedCalculator.Calculate(state);
        damageReduction *= _damageReductionWhileInjuredCalculator.Calculate(state);
        damageReduction *= _aggressiveResistance.GetDamageReduction(state);
        damageReduction *= _challengingShout.GetDamageReduction(state);
        damageReduction *= _gutteralYell.GetDamageReduction(state);
        damageReduction *= _aspectOfTheIronWarrior.GetDamageReductionBonus(state);
        damageReduction *= _ironBloodAspect.GetDamageReductionBonus(state);
        damageReduction *= _undaunted.GetDamageReduction(state);

        _log.Verbose($"Total Damage Reduction = {damageReduction:F2}x");

        return damageReduction;
    }
}
