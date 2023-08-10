using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class ProlificFury
{
    // While Berserking, Fury generation is increased by X%[x]
    public ProlificFury(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetFuryGeneration(SimulationState state)
    {
        if (!state.Player.Auras.Contains(Aura.Berserking))
        {
            return 1.0;
        }

        var skillPoints = state.Config.GetSkillPoints(Skill.ProlificFury);

        var result = skillPoints switch
        {
            1 => 1.06,
            2 => 1.12,
            >= 3 => 1.18,
            _ => 1.0,
        };

        if (result > 1.0)
        {
            _log.Verbose($"Fury Generation Bonus from Prolific Fury = {result:F2}x with {skillPoints} skill points");
        }

        return result;
    }
}
