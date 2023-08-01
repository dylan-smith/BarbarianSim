using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class StrengthCalculator
{
    public virtual double Calculate(SimulationState state)
    {
        var strength = state.Config.GetStatTotal(g => g.Strength);
        strength += state.Config.GetStatTotal(g => g.AllStats);
        strength += state.Config.PlayerSettings.Strength;
        strength += state.Config.PlayerSettings.Level - 1;

        return strength;
    }

    // Strength increases Skill Damage by 0.1% per point
    public virtual double GetStrengthMultiplier(SimulationState state, SkillType skillType)
    {
        if (skillType != SkillType.None)
        {
            var strength = Calculate(state);
            return 1 + (strength * 0.001);
        }

        return 1.0;
    }
}
