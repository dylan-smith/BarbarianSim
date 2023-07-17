using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class PhysicalDamageCalculator
{
    public virtual double Calculate(SimulationState state, DamageType damageType) => damageType == DamageType.Physical ? state.Config.Gear.GetStatTotal(g => g.PhysicalDamage) : 0.0;
}
