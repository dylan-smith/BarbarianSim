namespace BarbarianSim.StatCalculators;

public class CritChancePhysicalAgainstElitesCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType) => Calculate<CritChancePhysicalAgainstElitesCalculator>(state, damageType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType)
    {
        if (state.Config.EnemySettings.IsElite && damageType == DamageType.Physical)
        {
            var critChance = state.Config.Gear.AllGear.Sum(g => g.CritChancePhysicalAgainstElites);
            return critChance / 100.0;
        }

        return 0.0;
    }
}
