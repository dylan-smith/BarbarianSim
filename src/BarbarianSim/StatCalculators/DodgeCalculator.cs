namespace BarbarianSim.StatCalculators;

public class DodgeCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DodgeCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var dodge = state.Config.Gear.GetStatTotal(g => g.Dodge);
        dodge += DexterityCalculator.Calculate(state) * 0.01;

        return dodge / 100.0;
    }
}
