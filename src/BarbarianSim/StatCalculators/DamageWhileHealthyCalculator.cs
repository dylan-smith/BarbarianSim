using BarbarianSim.Arsenal;
using BarbarianSim.Config;

namespace BarbarianSim.StatCalculators;

public class DamageWhileHealthyCalculator
{
    public DamageWhileHealthyCalculator(MaxLifeCalculator maxLifeCalculator, PolearmExpertise polearmExpertise)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _polearmExpertise = polearmExpertise;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly PolearmExpertise _polearmExpertise;

    public virtual double Calculate(SimulationState state, GearItem weapon)
    {
        var damage = _polearmExpertise.GetHealthyDamageMultiplier(weapon);

        return state.Player.IsHealthy(_maxLifeCalculator.Calculate(state)) ? damage : 1.0;
    }
}
