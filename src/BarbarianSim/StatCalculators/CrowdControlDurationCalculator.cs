using BarbarianSim.Aspects;

namespace BarbarianSim.StatCalculators;

public class CrowdControlDurationCalculator
{
    public CrowdControlDurationCalculator(SmitingAspect smitingAspect, ExploitersAspect exploitersAspect, SimLogger log)
    {
        _smitingAspect = smitingAspect;
        _exploitersAspect = exploitersAspect;
        _log = log;
    }

    private readonly SmitingAspect _smitingAspect;
    private readonly ExploitersAspect _exploitersAspect;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, double duration)
    {
        var multiplier = 1.0;
        multiplier += _exploitersAspect.GetCrowdControlDuration(state) / 100.0;

        multiplier *= _smitingAspect.GetCrowdControlDurationBonus(state);

        var result = duration * multiplier;

        if (result != duration)
        {
            _log.Verbose($"Crowd Control Duration increased from {duration:F2} to {result:F2} (multiplier = {multiplier:F2}x");
        }

        return duration * multiplier;
    }
}
