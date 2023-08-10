namespace BarbarianSim.StatCalculators;

public class AttackSpeedCalculator
{

    public AttackSpeedCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var attackSpeed = state.Config.GetStatTotal(g => g.AttackSpeed);
        var result = 1.0 / (1.0 + (attackSpeed / 100.0));

        if (result != 1.0)
        {
            _log.Verbose($"Attack Speed Multiplier = {result:F2}x");
        }

        return result;
    }
}
