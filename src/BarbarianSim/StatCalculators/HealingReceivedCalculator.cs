namespace BarbarianSim.StatCalculators;

public class HealingReceivedCalculator
{
    public HealingReceivedCalculator(WillpowerCalculator willpowerCalculator, SimLogger log)
    {
        _willpowerCalculator = willpowerCalculator;
        _log = log;
    }

    private readonly WillpowerCalculator _willpowerCalculator;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var healingFromConfig = state.Config.GetStatTotal(g => g.HealingReceived);
        if (healingFromConfig > 0)
        {
            _log.Verbose($"Healing Received from Config = {healingFromConfig:F2}%");
        }

        var healingFromWillpower = _willpowerCalculator.Calculate(state) * 0.1;
        if (healingFromWillpower > 0)
        {
            _log.Verbose($"Healing Received from Willpower = {healingFromWillpower:F2}%");
        }

        var result = 1.0 + ((healingFromConfig + healingFromWillpower) / 100.0);
        if (result != 1.0)
        {
            _log.Verbose($"Total Healing Received = {result:F2}x");
        }

        return result;
    }
}
