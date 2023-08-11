using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class PhysicalDamageCalculator
{
    public PhysicalDamageCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, DamageType damageType)
    {
        if (damageType == DamageType.Physical)
        {
            var result = state.Config.GetStatTotal(g => g.PhysicalDamage);
            if (result > 0)
            {
                _log.Verbose($"Physical Damage = {result:F2}%");
            }

            return result;
        }

        return 0;
    }
}
