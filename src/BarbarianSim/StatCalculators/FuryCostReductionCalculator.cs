using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class FuryCostReductionCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, SkillType skillType) => Calculate<FuryCostReductionCalculator>(state, skillType);

    protected override double InstanceCalculate(SimulationState state, SkillType skillType)
    {
        var furyCostReduction = state.Config.Gear.AllGear.Sum(g => g.FuryCostReduction);

        var result = 1.0 - (furyCostReduction / 100);

        if (state.Config.Skills.ContainsKey(Skill.UnbridledRage))
        {
            if (skillType == SkillType.Core)
            {
                result *= 2;
            }
        }

        return result;
    }
}
