using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class TwoHandedWeaponDamageMultiplicativeCalculator
{
    public TwoHandedWeaponDamageMultiplicativeCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, GearItem weapon)
    {
        if (weapon.Expertise.IsTwoHanded())
        {
            var result = 1 + (state.Config.GetStatTotal(g => g.TwoHandWeaponDamageMultiplicative) / 100.0);
            if (result > 1.0)
            {
                _log.Verbose($"Two-Handed Weapon Damage = {result:F2}x");
            }
            return result;
        }

        return 1.0;
    }
}
