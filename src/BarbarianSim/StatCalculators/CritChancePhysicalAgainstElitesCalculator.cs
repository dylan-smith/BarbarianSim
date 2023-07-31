using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class CritChancePhysicalAgainstElitesCalculator
{
    public virtual double Calculate(SimulationState state, DamageType damageType)
    {
        if (state.Config.EnemySettings.IsElite && damageType == DamageType.Physical)
        {
            var critChance = state.Config.GetStatTotal(g => g.CritChancePhysicalAgainstElites);
            return critChance / 100.0;
        }

        return 0.0;
    }
}
