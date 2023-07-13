using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class TemperedFury
{
    // Increase your Maximum Fury by 3

    public double GetMaximumFury(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.TemperedFury))
        {
            skillPoints += state.Config.Skills[Skill.TemperedFury];
        }

        return skillPoints switch
        {
            1 => 3,
            2 => 6,
            >= 3 => 9,
            _ => 0,
        };
    }
}
