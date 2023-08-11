namespace BarbarianSim.StatCalculators;

public class DamageReductionFromCloseCalculator
{
    public DamageReductionFromCloseCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var result = state.Config.GetStatTotalMultiplied(g => 1 - (g.DamageReductionFromClose / 100));

        if (result != 1)
        {
            _log.Verbose($"Damage Reduction from Close = {result:F2}x");
        }

        return result;
    }
}
