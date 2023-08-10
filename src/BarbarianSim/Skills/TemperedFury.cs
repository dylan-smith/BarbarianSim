using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class TemperedFury
{
    // Increase your Maximum Fury by X
    public TemperedFury(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetMaximumFury(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.TemperedFury);

        var result = skillPoints switch
        {
            1 => 3,
            2 => 6,
            >= 3 => 9,
            _ => 0,
        };

        if (result > 0)
        {
            _log.Verbose($"Maximum Fury Bonus from Tempered Fury = {result:F2} with {skillPoints} skill points");
        }

        return result;
    }
}
