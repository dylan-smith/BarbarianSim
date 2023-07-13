using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class PitFighter
{
    // You deal 3%[x] increased damage to Close enemies and gain 2% Distant Damage Reduction

    public double GetCloseDamageBonus(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.PitFighter))
        {
            skillPoints += state.Config.Skills[Skill.PitFighter];
        }

        return skillPoints switch
        {
            1 => 1.03,
            2 => 1.06,
            >= 3 => 1.09,
            _ => 1.0,
        };
    }
}
