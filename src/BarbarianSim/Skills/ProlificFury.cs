using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class ProlificFury
{
    // While Berserking, Fury generation is increased by 6%[x]

    public double GetFuryGeneration(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Player.Auras.Contains(Aura.Berserking) && state.Config.Skills.ContainsKey(Skill.ProlificFury))
        {
            skillPoints += state.Config.Skills[Skill.ProlificFury];
        }

        return skillPoints switch
        {
            1 => 1.06,
            2 => 1.12,
            >= 3 => 1.18,
            _ => 1.0,
        };
    }
}
