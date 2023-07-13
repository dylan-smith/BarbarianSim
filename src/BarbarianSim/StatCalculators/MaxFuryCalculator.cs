using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MaxFuryCalculator
{
    public MaxFuryCalculator(TemperedFury temperedFury) => _temperedFury = temperedFury;

    private readonly TemperedFury _temperedFury;

    public double Calculate(SimulationState state)
    {
        var maxFury = 100.0;
        maxFury += state.Config.Gear.GetStatTotal(g => g.MaxFury);
        maxFury += _temperedFury.GetMaximumFury(state);

        return maxFury;
    }
}
