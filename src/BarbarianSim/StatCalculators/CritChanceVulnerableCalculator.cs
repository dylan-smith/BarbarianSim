using BarbarianSim.Arsenal;
using BarbarianSim.Config;

namespace BarbarianSim.StatCalculators;

public class CritChanceVulnerableCalculator
{
    public CritChanceVulnerableCalculator(TwoHandedAxeExpertise twoHandedAxeExpertise, SimLogger log)
    {
        _twoHandedAxeExpertise = twoHandedAxeExpertise;
        _log = log;
    }

    private readonly TwoHandedAxeExpertise _twoHandedAxeExpertise;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, EnemyState enemy, GearItem weapon)
    {
        if (enemy.IsVulnerable())
        {
            var result = _twoHandedAxeExpertise.GetCritChanceVulnerable(state, weapon);

            if (result > 0)
            {
                _log.Verbose($"Crit Chance against Vulnerable = {result:F2}%");
            }

            return result;
        }

        return 0.0;
    }
}
