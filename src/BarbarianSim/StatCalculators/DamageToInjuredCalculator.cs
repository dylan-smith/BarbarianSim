namespace BarbarianSim.StatCalculators;

public class DamageToInjuredCalculator
{
    public DamageToInjuredCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, EnemyState enemy)
    {
        if (enemy.IsInjured())
        {
            var result = state.Config.GetStatTotal(g => g.DamageToInjured);
            _log.Verbose($"Damage to Injured = {result:F2}%");

            return result;
        }

        return 0;
    }
}
