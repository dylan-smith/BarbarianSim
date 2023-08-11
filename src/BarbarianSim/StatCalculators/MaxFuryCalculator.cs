using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MaxFuryCalculator
{
    public MaxFuryCalculator(TemperedFury temperedFury, SimLogger log)
    {
        _temperedFury = temperedFury;
        _log = log;
    }

    private readonly TemperedFury _temperedFury;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var maxFury = 100.0;
        var maxFuryFromConfig = state.Config.GetStatTotal(g => g.MaxFury);
        if (maxFuryFromConfig > 0)
        {
            _log.Verbose($"Max Fury from Config = {maxFuryFromConfig:F2}");
        }

        var maxFuryTemperedFury = _temperedFury.GetMaximumFury(state);

        maxFury += maxFuryFromConfig + maxFuryTemperedFury;
        _log.Verbose($"Total Max Fury = {maxFury:F2} (Current Fury: {state.Player.Fury:F2})");

        return maxFury;
    }
}
