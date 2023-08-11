namespace BarbarianSim.StatCalculators;

public class DamageToCrowdControlledCalculator
{
    public DamageToCrowdControlledCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, EnemyState enemy)
    {
        if (enemy.IsCrowdControlled())
        {
            var result = state.Config.GetStatTotal(g => g.DamageToCrowdControlled);
            _log.Verbose($"Damage to Crowd Controlled = {result:F2}%");

            return result;
        }

        return 0;
    }
}
