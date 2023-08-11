namespace BarbarianSim.StatCalculators;

public class OverpowerDamageCalculator
{
    public OverpowerDamageCalculator(WillpowerCalculator willpowerCalculator, SimLogger log)
    {
        _willpowerCalculator = willpowerCalculator;
        _log = log;
    }

    private readonly WillpowerCalculator _willpowerCalculator;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var damageFromConfig = state.Config.GetStatTotal(g => g.OverpowerDamage);
        if (damageFromConfig > 0)
        {
            _log.Verbose($"Overpower Damage from Config = {damageFromConfig:F2}%");
        }

        var damageFromWillpower = _willpowerCalculator.Calculate(state) * 0.25;
        if (damageFromWillpower > 0)
        {
            _log.Verbose($"Overpower Damage from Willpower = {damageFromWillpower:F2}%");
        }

        var result = damageFromConfig + damageFromWillpower;
        result = 1.0 + (result / 100.0);

        if (result > 1.0)
        {
            _log.Verbose($"Total Overpower Damage = {result:F2}x");
        }

        return result;
    }
}
