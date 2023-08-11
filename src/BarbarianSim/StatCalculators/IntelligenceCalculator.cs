namespace BarbarianSim.StatCalculators;

public class IntelligenceCalculator
{
    public IntelligenceCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var intFromConfig = state.Config.GetStatTotal(g => g.Intelligence);
        if (intFromConfig > 0)
        {
            _log.Verbose($"Intelligence from Config = {intFromConfig:F2}");
        }

        var intFromAllStats = state.Config.GetStatTotal(g => g.AllStats);
        if (intFromAllStats > 0)
        {
            _log.Verbose($"Intelligence from All Stats = {intFromAllStats:F2}");
        }

        var baseInt = state.Config.PlayerSettings.Intelligence;
        if (baseInt > 0)
        {
            _log.Verbose($"Base Intelligence = {baseInt:F2}");
        }

        var intFromLevel = state.Config.PlayerSettings.Level - 1;
        if (intFromLevel > 0)
        {
            _log.Verbose($"Intelligence from Level = {intFromLevel:F2}");
        }

        var result = intFromConfig + intFromAllStats + baseInt + intFromLevel;
        if (result > 0)
        {
            _log.Verbose($"Total Intelligence = {result:F2}");
        }

        return result;
    }
}
