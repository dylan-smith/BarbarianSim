using BarbarianSim.Arsenal;
using BarbarianSim.Config;

namespace BarbarianSim.StatCalculators;

public class VulnerableDamageBonusCalculator
{
    public VulnerableDamageBonusCalculator(TwoHandedAxeExpertise twoHandedAxeExpertise) => _twoHandedAxeExpertise = twoHandedAxeExpertise;

    private readonly TwoHandedAxeExpertise _twoHandedAxeExpertise;

    public virtual double Calculate(SimulationState state, EnemyState enemy, GearItem weapon)
    {
        if (!enemy.IsVulnerable())
        {
            return 1.0;
        }

        var vulnerableDamage = 1.2;
        vulnerableDamage += state.Config.GetStatTotal(g => g.VulnerableDamage) / 100.0;

        vulnerableDamage *= _twoHandedAxeExpertise.GetVulnerableDamageMultiplier(state, weapon);

        return vulnerableDamage;
    }
}
