namespace BarbarianSim.StatCalculators;

public class DamageReductionWhileFortifiedCalculator
{
    private const double BASE_FORTIFY_DAMAGE_REDUCTION = 10;

    public DamageReductionWhileFortifiedCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        if (state.Player.IsFortified())
        {
            var result = 1 - (BASE_FORTIFY_DAMAGE_REDUCTION / 100);
            _log.Verbose($"Base Fortify Damage Reduction = {result:F2}x");

            var damageReductionFromConfig = state.Config.GetStatTotalMultiplied(g => 1 - (g.DamageReductionWhileFortified / 100));

            if (damageReductionFromConfig != 1)
            {
                result *= damageReductionFromConfig;
                _log.Verbose($"Damage Reduction While Fortified from Config = {damageReductionFromConfig:F2}x");
            }

            _log.Verbose($"Total Damage Reduction While Fortified = {result:F2}x");

            return result;
        }

        return 1;
    }
}
