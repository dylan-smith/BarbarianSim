using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class TwoHandedWeaponDamageMultiplicativeCalculator
{
    public virtual double Calculate(SimulationState state, GearItem weapon) =>
        weapon?.Expertise is Expertise.TwoHandedAxe or Expertise.TwoHandedMace or Expertise.TwoHandedSword
            ? 1 + (state.Config.GetStatTotal(g => g.TwoHandWeaponDamageMultiplicative) / 100.0)
            : 1.0;
}
