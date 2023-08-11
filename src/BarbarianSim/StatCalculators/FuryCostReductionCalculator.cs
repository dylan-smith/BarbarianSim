using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class FuryCostReductionCalculator
{
    public FuryCostReductionCalculator(UnbridledRage unbridledRage, SimLogger log)
    {
        _unbridledRage = unbridledRage;
        _log = log;
    }

    private readonly UnbridledRage _unbridledRage;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, SkillType skillType)
    {
        var furyCostReduction = state.Config.GetStatTotal(g => g.FuryCostReduction);

        if (furyCostReduction > 0)
        {
            _log.Verbose($"Fury Cost Reduction bonus from Gear/Paragon = {furyCostReduction:F2}%");
        }

        var result = 1.0 - (furyCostReduction / 100);

        result *= _unbridledRage.GetFuryCostReduction(state, skillType); // this is really a fury cost increase

        if (result != 1.0)
        {
            _log.Verbose($"Total Fury Cost Reduction multiplier = {result:F2}");
        }

        return result;
    }
}
