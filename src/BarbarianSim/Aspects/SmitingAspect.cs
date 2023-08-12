using BarbarianSim.Config;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Aspects;

public class SmitingAspect : Aspect
{
    // You have [10 - 20]%[x] increased Critical Strike Chance against Injured enemies. While you are Healthy, you gain [20 - 40]%[x] increased Crowd Control Duration.
    public double CritChance { get; set; }
    public double CrowdControlDuration { get; set; }

    public SmitingAspect(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public virtual double GetCriticalStrikeChanceBonus(SimulationState state, EnemyState enemy)
    {
        if (IsAspectEquipped(state) && enemy.IsInjured())
        {
            var result = 1 + (CritChance / 100.0);
            _log.Verbose($"Smiting Aspect Critical Strike Chance Bonus = {result:F2}x");

            return result;
        }

        return 1.0;
    }

    public virtual double GetCrowdControlDurationBonus(SimulationState state)
    {
        if (IsAspectEquipped(state) && state.Player.IsHealthy(_maxLifeCalculator.Calculate(state)))
        {
            var result = 1 + (CrowdControlDuration / 100.0);
            _log.Verbose($"Smiting Aspect Crowd Control Duration Bonus = {result:F2}x");

            return result;
        }

        return 1.0;
    }
}
