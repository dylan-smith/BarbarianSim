using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.Arsenal;

public class PolearmExpertise
{
    // 10%[x] increased Lucky Hit Chance
    // Rank 10 Bonus: 10%[x] increased damage while Healthy
    public const double LUCKY_HIT_CHANCE = 1.1;
    public const double HEALTHY_DAMAGE = 1.1;

    public virtual double GetLuckyHitChanceMultiplier(SimulationState state, GearItem weapon)
    {
        return weapon?.Expertise == Expertise.Polearm || state.Config.PlayerSettings.ExpertiseTechnique == Expertise.Polearm
            ? LUCKY_HIT_CHANCE
            : 1.0;
    }

    public virtual double GetHealthyDamageMultiplier(GearItem weapon)
    {
        return weapon?.Expertise == Expertise.Polearm
            ? HEALTHY_DAMAGE
            : 1.0;
    }
}
