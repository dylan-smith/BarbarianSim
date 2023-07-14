using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MaxFuryCalculator
{
    public double Calculate(SimulationState state)
    {
        var maxFury = 100.0;
        maxFury += state.Config.Gear.GetStatTotal(g => g.MaxFury);
        maxFury += TemperedFury.GetMaximumFury(state);

        return maxFury;
    }
}
