using BarbarianSim.Aspects;

namespace BarbarianSim.StatCalculators;

public class CrowdControlDurationCalculator
{
    public CrowdControlDurationCalculator(SmitingAspect smitingAspect) => _smitingAspect = smitingAspect;

    private readonly SmitingAspect _smitingAspect;

    public virtual double Calculate(SimulationState state) => _smitingAspect.GetCrowdControlDurationBonus(state);
}
