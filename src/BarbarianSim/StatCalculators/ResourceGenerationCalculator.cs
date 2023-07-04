namespace BarbarianSim.StatCalculators;

public class ResourceGenerationCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<ResourceGenerationCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var resourceGeneration = state.Config.Gear.GetStatTotal(g => g.ResourceGeneration);
        resourceGeneration += WillpowerCalculator.Calculate(state) * 0.03;

        return 1.0 + (resourceGeneration / 100.0);
    }
}
