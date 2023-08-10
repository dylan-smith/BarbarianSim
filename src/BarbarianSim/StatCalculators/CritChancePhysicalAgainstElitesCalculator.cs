using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class CritChancePhysicalAgainstElitesCalculator
{
    public virtual double Calculate(SimulationState state, DamageType damageType)
    {
        if (state.Config.EnemySettings.IsElite && damageType == DamageType.Physical)
        {
            return state.Config.GetStatTotal(g => g.CritChancePhysicalAgainstElites);
        }

        return 0.0;
    }
}
