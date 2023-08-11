using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class StrengthCalculator
{
    public StrengthCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var strengthFromConfig = state.Config.GetStatTotal(g => g.Strength);
        if (strengthFromConfig > 0)
        {
            _log.Verbose($"Strength from Config = {strengthFromConfig:F2}");
        }

        var strengthFromAllStats = state.Config.GetStatTotal(g => g.AllStats);
        if (strengthFromAllStats > 0)
        {
            _log.Verbose($"Strength from All Stats from Config = {strengthFromAllStats:F2}");
        }

        var baseStrength = state.Config.PlayerSettings.Strength;
        if (baseStrength > 0)
        {
            _log.Verbose($"Base Strength = {baseStrength:F2}");
        }

        var strengthFromLevel = state.Config.PlayerSettings.Level - 1;
        if (strengthFromLevel > 0)
        {
            _log.Verbose($"Strength from Level = {strengthFromLevel:F2}");
        }

        var result = strengthFromConfig + strengthFromAllStats + baseStrength + strengthFromLevel;
        _log.Verbose($"Total Strength = {result:F2}");

        return result;
    }

    // Strength increases Skill Damage by 0.1% per point
    public virtual double GetDamageMultiplier(SimulationState state, SkillType skillType)
    {
        if (skillType != SkillType.None)
        {
            var strength = Calculate(state);
            var result = 1 + (strength * 0.001);

            _log.Verbose($"Strength Damage Multiplier = {result:F2}x");

            return result;
        }

        return 1.0;
    }
}
