using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public static class AggressiveResistance
{
    // Gain 3% Damage Reduction while Berserking

    public static double GetDamageReduction(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.AggressiveResistance) && state.Player.Auras.Contains(Aura.Berserking))
        {
            skillPoints += state.Config.Skills[Skill.AggressiveResistance];
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
