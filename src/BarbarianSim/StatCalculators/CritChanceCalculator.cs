namespace BarbarianSim.StatCalculators;

public class CritChanceCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType) => Calculate<CritChanceCalculator>(state, damageType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType)
    {
        var critChance = state.Config.Gear.AllGear.Sum(g => g.CritChance);

        critChance += CritChancePhysicalAgainstElitesCalculator.Calculate(state, damageType);

        return critChance / 100.0;
    }
}
