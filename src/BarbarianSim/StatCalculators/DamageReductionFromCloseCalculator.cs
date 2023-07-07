namespace BarbarianSim.StatCalculators;

public class DamageReductionFromCloseCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DamageReductionFromCloseCalculator>(state);

    protected override double InstanceCalculate(SimulationState state) => state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReductionFromClose / 100));
}
