using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class DamageReductionFromBleedingCalculator
{
    public DamageReductionFromBleedingCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, EnemyState enemy)
    {
        if (enemy.Auras.Contains(Aura.Bleeding))
        {
            var damageReduction = state.Config.GetStatTotalMultiplied(g => 1 - (g.DamageReductionFromBleeding / 100));

            if (damageReduction != 1.0)
            {
                _log.Verbose($"Damage Reduction from Bleeding = {damageReduction:F2}x");
            }

            return damageReduction;
        }

        return 1;
    }
}
