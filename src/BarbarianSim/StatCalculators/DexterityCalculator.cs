namespace BarbarianSim.StatCalculators;

public class DexterityCalculator
{
    public DexterityCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var dexterityFromConfig = state.Config.GetStatTotal(g => g.Dexterity);
        _log.Verbose($"Dexterity from Config = {dexterityFromConfig:F2}");

        var allStatsFromConfig = state.Config.GetStatTotal(g => g.AllStats);
        _log.Verbose($"Dexterity from All Stats from Config = {allStatsFromConfig:F2}");

        var baseDexterity = state.Config.PlayerSettings.Dexterity;
        _log.Verbose($"Base Dexterity = {baseDexterity:F2}");

        var dexterityFromLevel = state.Config.PlayerSettings.Level - 1;
        _log.Verbose($"Dexterity from Level = {dexterityFromLevel:F2}");

        var result = dexterityFromConfig + allStatsFromConfig + baseDexterity + dexterityFromLevel;
        _log.Verbose($"Total Dexterity = {result:F2}");

        return result;
    }
}
