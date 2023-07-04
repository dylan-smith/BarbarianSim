namespace BarbarianSim.StatCalculators;

public class DamageToCloseCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DamageToCloseCalculator>(state);

    protected override double InstanceCalculate(SimulationState state) => state.Config.Gear.GetStatTotal(g => g.DamageToClose);
}
