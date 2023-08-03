using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Arsenal;

public class TwoHandedAxeExpertise
{
    // 15%[x] increased damage to Vulnerable enemies
    // Rank 10 Bonus: 10%[+] increased Critical Strike Chance against Vulnerable enemies
    public virtual double GetVulnerableDamageMultiplier(SimulationState state, GearItem weapon)
    {
        return state.Config.PlayerSettings.ExpertiseTechnique == Expertise.TwoHandedAxe || weapon?.Expertise == Expertise.TwoHandedAxe
            ? 1.15
            : 1.0;
    }

    public virtual double GetCritChanceVulnerable(SimulationState state, GearItem weapon)
    {
        return weapon?.Expertise == Expertise.TwoHandedAxe
            ? 10
            : 0.0;
    }
}
