namespace BarbarianSim.StatCalculators;

public class DamageToSlowedCalculator
{
    public DamageToSlowedCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, EnemyState enemy)
    {
        if (enemy.IsSlowed())
        {
            var result = state.Config.GetStatTotal(g => g.DamageToSlowed);
            _log.Verbose($"Damage to Slowed = {result:F2}%");

            return result;
        }

        return 0;
    }
}
