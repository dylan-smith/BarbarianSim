using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Paragon;

public class Undaunted
{
    // You gain up to 10% Damage Reduction the more Fortify you have.
    public const double MAX_DAMAGE_REDUCTION = 10;

    public Undaunted(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public virtual double GetDamageReduction(SimulationState state)
    {
        if (state.Config.HasParagonNode(ParagonNode.Undaunted))
        {
            var maxLife = _maxLifeCalculator.Calculate(state);
            var result = MAX_DAMAGE_REDUCTION * (state.Player.Fortify / maxLife);
            _log.Verbose($"Player Fortify = {state.Player.Fortify:F2}");
            _log.Verbose($"Undaunted Damage Reduction = {result:F2}%");
            return result;
        }

        return 0.0;
    }
}
