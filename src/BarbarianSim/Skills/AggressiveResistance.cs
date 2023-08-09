using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class AggressiveResistance
{
    // Gain 3% Damage Reduction while Berserking

    public AggressiveResistance(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetDamageReduction(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.AggressiveResistance) && state.Player.Auras.Contains(Aura.Berserking))
        {
            skillPoints += state.Config.Skills[Skill.AggressiveResistance];
        }

        var result = skillPoints switch
        {
            1 => 3,
            2 => 6,
            >= 3 => 9,
            _ => 0,
        };

        _log.Verbose($"Damage Reduction from Aggressive Resistance = {result}% with {skillPoints} skill points");

        return result;
    }
}
