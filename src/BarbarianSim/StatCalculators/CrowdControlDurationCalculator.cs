using BarbarianSim.Aspects;

namespace BarbarianSim.StatCalculators;

public class CrowdControlDurationCalculator
{
    public CrowdControlDurationCalculator(SmitingAspect smitingAspect, ExploitersAspect exploitersAspect)
    {
        _smitingAspect = smitingAspect;
        _exploitersAspect = exploitersAspect;
    }

    private readonly SmitingAspect _smitingAspect;
    private readonly ExploitersAspect _exploitersAspect;

    public virtual double Calculate(SimulationState state, double duration)
    {
        var multiplier = 1.0;
        multiplier += _exploitersAspect.GetCrowdControlDuration(state);

        multiplier *= _smitingAspect.GetCrowdControlDurationBonus(state);

        return duration * multiplier;
    }
}
