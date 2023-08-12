using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Arsenal;

public class TwoHandedAxeExpertise
{
    // 15%[x] increased damage to Vulnerable enemies
    // Rank 10 Bonus: 10%[+] increased Critical Strike Chance against Vulnerable enemies
    public const double VULNERABLE_DAMAGE = 1.15;
    public const double VULNERABLE_CRIT_CHANCE = 10;

    public TwoHandedAxeExpertise(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetVulnerableDamageMultiplier(SimulationState state, GearItem weapon)
    {
        if (state.Config.PlayerSettings.ExpertiseTechnique == Expertise.TwoHandedAxe || weapon?.Expertise == Expertise.TwoHandedAxe)
        {
            _log.Verbose($"Two-Handed Axe Expertise Vulnerable Damage = {VULNERABLE_DAMAGE:F2}x");
            return VULNERABLE_DAMAGE;
        }

        return 1.0;
    }

    public virtual double GetCritChanceVulnerable(SimulationState state, GearItem weapon)
    {
        if (weapon?.Expertise == Expertise.TwoHandedAxe)
        {
            _log.Verbose($"Two-Handed Axe Expertise Vulnerable Crit Chance = {VULNERABLE_CRIT_CHANCE:F2}%");
            return VULNERABLE_CRIT_CHANCE;
        }

        return 0.0;
    }
}
