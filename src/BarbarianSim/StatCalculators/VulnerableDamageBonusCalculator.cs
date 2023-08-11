using BarbarianSim.Arsenal;
using BarbarianSim.Config;

namespace BarbarianSim.StatCalculators;

public class VulnerableDamageBonusCalculator
{
    public VulnerableDamageBonusCalculator(TwoHandedAxeExpertise twoHandedAxeExpertise, SimLogger log)
    {
        _twoHandedAxeExpertise = twoHandedAxeExpertise;
        _log = log;
    }

    private readonly TwoHandedAxeExpertise _twoHandedAxeExpertise;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, EnemyState enemy, GearItem weapon)
    {
        if (!enemy.IsVulnerable())
        {
            return 1.0;
        }

        var vulnerableDamage = 1.2;
        _log.Verbose($"Base Vulnerable Damage = {vulnerableDamage:F2}x");

        var damageFromConfig = state.Config.GetStatTotal(g => g.VulnerableDamage) / 100.0;
        if (damageFromConfig > 0)
        {
            _log.Verbose($"Vulnerable Damage from Config = {damageFromConfig:P2}");
            vulnerableDamage += damageFromConfig;
        }

        vulnerableDamage *= _twoHandedAxeExpertise.GetVulnerableDamageMultiplier(state, weapon);

        _log.Verbose($"Total Vulnerable Damage = {vulnerableDamage:F2}x");

        return vulnerableDamage;
    }
}
