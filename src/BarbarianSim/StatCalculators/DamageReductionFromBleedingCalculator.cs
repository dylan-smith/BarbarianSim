using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class DamageReductionFromBleedingCalculator
{
    public virtual double Calculate(SimulationState state, EnemyState enemy)
    {
        return enemy.Auras.Contains(Aura.Bleeding) ?
            state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReductionFromBleeding / 100)) :
            1;
    }
}
