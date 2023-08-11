using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Arsenal;

public class PolearmExpertise
{
    // 10%[x] increased Lucky Hit Chance
    // Rank 10 Bonus: 10%[x] increased damage while Healthy
    public const double LUCKY_HIT_CHANCE = 1.1;
    public const double HEALTHY_DAMAGE = 1.1;

    public PolearmExpertise(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public virtual double GetLuckyHitChanceMultiplier(SimulationState state, GearItem weapon)
    {
        if (weapon?.Expertise == Expertise.Polearm || state.Config.PlayerSettings.ExpertiseTechnique == Expertise.Polearm)
        {
            _log.Verbose($"Polearm Expertise Lucky Hit Chance Bonus = {LUCKY_HIT_CHANCE:F2}x");
            return LUCKY_HIT_CHANCE;
        }

        return 1.0;
    }

    public virtual double GetHealthyDamageMultiplier(SimulationState state, GearItem weapon)
    {
        if (weapon?.Expertise == Expertise.Polearm && state.Player.IsHealthy(_maxLifeCalculator.Calculate(state)))
        {
            _log.Verbose($"Polearm Expertise Healthy Damage = {HEALTHY_DAMAGE:F2}x");
            return HEALTHY_DAMAGE;
        }

        return 1.0;
    }
}
