namespace BarbarianSim.StatCalculators;

public class DamageToCloseCalculator
{
    public DamageToCloseCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var result = state.Config.GetStatTotal(g => g.DamageToClose);
        _log.Verbose($"Damage to Close = {result:F2}%");

        return result;
    }
}
