using BarbarianSim.Config;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Aspects;

public class SmitingAspect : Aspect
{
    // You have [10 - 20]%[x] increased Critical Strike Chance against Injured enemies. While you are Healthy, you gain [20 - 40]%[x] increased Crowd Control Duration.
    public double CritChance { get; set; }
    public double CrowdControlDuration { get; set; }

    public SmitingAspect(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public virtual double GetCriticalStrikeChanceBonus(SimulationState state, EnemyState enemy)
    {
        return IsAspectEquipped(state) && enemy.IsInjured()
            ? 1 + (CritChance / 100.0)
            : 1.0;
    }

    public virtual double GetCrowdControlDurationBonus(SimulationState state)
    {
        return IsAspectEquipped(state) &&
            state.Player.IsHealthy(_maxLifeCalculator.Calculate(state))
            ? 1 + (CrowdControlDuration / 100.0)
            : 1.0;
    }
}
