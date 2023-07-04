using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class CritChanceCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType) => Calculate<CritChanceCalculator>(state, damageType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType)
    {
        var critChance = 5.0; // base chance to crit
        critChance += state.Config.Gear.AllGear.Sum(g => g.CritChance);
        critChance += CritChancePhysicalAgainstElitesCalculator.Calculate(state, damageType);
        critChance += DexterityCalculator.Calculate(state) * 0.02;

        return critChance / 100.0;
    }
}
