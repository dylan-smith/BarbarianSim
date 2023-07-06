namespace BarbarianSim.StatCalculators;

public class FuryCostReductionCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<FuryCostReductionCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var furyCostReduction = state.Config.Gear.AllGear.Sum(g => g.FuryCostReduction);

        return 1.0 - (furyCostReduction / 100);
    }
}
