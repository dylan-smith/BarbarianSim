using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class FuryCostReductionCalculator
{
    public FuryCostReductionCalculator(UnbridledRage unbridledRage) => _unbridledRage = unbridledRage;

    private readonly UnbridledRage _unbridledRage;

    public virtual double Calculate(SimulationState state, SkillType skillType)
    {
        var furyCostReduction = state.Config.Gear.AllGear.Sum(g => g.FuryCostReduction);

        var result = 1.0 - (furyCostReduction / 100);

        result *= _unbridledRage.GetFuryCostReduction(state, skillType);

        return result;
    }
}
