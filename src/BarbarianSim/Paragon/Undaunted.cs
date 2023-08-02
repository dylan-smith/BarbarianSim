using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Paragon;

public class Undaunted
{
    // You gain up to 10% Damage Reduction the more Fortify you have.
    public const double MAX_DAMAGE_REDUCTION = 10;

    public Undaunted(MaxLifeCalculator maxLifeCalculator)
    {
        _maxLifeCalculator = maxLifeCalculator;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public virtual double GetDamageReduction(SimulationState state)
    {
        return !state.Config.HasParagonNode(ParagonNode.Undaunted)
            ? 0.0
            : MAX_DAMAGE_REDUCTION * (state.Player.Fortify / _maxLifeCalculator.Calculate(state));
    }
}
