using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class CritChancePhysicalAgainstElitesCalculator
{
    public CritChancePhysicalAgainstElitesCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, DamageType damageType)
    {
        if (state.Config.EnemySettings.IsElite && damageType == DamageType.Physical)
        {
            var result = state.Config.GetStatTotal(g => g.CritChancePhysicalAgainstElites);

            if (result > 0)
            {
                _log.Verbose($"Crit Chance Physical Against Elites = {result:F2}%");
            }

            return result;
        }

        return 0.0;
    }
}
