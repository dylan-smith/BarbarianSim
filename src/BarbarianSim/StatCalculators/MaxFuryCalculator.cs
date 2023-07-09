using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MaxFuryCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<MaxFuryCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var maxFury = 100.0;
        maxFury += state.Config.Gear.GetStatTotal(g => g.MaxFury);
        maxFury += TemperedFury.GetMaximumFury(state);

        return maxFury;
    }
}
