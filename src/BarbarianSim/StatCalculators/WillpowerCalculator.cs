namespace BarbarianSim.StatCalculators;

public class WillpowerCalculator
{
    public WillpowerCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var fromConfig = state.Config.GetStatTotal(g => g.Willpower);
        if (fromConfig > 0)
        {
            _log.Verbose($"Willpower from Config = {fromConfig:F2}");
        }

        var allStats = state.Config.GetStatTotal(g => g.AllStats);
        if (allStats > 0)
        {
            _log.Verbose($"Willpower from All Stats from Config = {allStats:F2}");
        }

        var baseWillpower = state.Config.PlayerSettings.Willpower;
        _log.Verbose($"Base Willpower = {baseWillpower:F2}");

        var levelWillpower = state.Config.PlayerSettings.Level - 1;
        _log.Verbose($"Willpower from Level = {levelWillpower:F2}");

        var result = fromConfig + allStats + baseWillpower + levelWillpower;
        _log.Verbose($"Total Willpower = {result:F2}");

        return result;
    }
}
