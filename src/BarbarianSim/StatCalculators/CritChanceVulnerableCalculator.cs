using BarbarianSim.Arsenal;
using BarbarianSim.Config;

namespace BarbarianSim.StatCalculators;

public class CritChanceVulnerableCalculator
{
    public CritChanceVulnerableCalculator(TwoHandedAxeExpertise twoHandedAxeExpertise) => _twoHandedAxeExpertise = twoHandedAxeExpertise;

    private readonly TwoHandedAxeExpertise _twoHandedAxeExpertise;

    public virtual double Calculate(SimulationState state, EnemyState enemy, GearItem weapon)
    {
        return enemy.IsVulnerable()
            ? _twoHandedAxeExpertise.GetCritChanceVulnerable(state, weapon)
            : 0.0;
    }
}
