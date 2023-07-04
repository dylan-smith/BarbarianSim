using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class PhysicalDamageCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType) => Calculate<PhysicalDamageCalculator>(state, damageType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType) => damageType == DamageType.Physical ? state.Config.Gear.GetStatTotal(g => g.PhysicalDamage) : 0.0;
}
