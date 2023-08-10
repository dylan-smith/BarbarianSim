using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class PitFighter
{
    // You deal 3%[x] increased damage to Close enemies and gain 2% Distant Damage Reduction
    public PitFighter(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetCloseDamageBonus(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.PitFighter);

        var result = skillPoints switch
        {
            1 => 1.03,
            2 => 1.06,
            >= 3 => 1.09,
            _ => 1.0,
        };

        if (result > 1.0)
        {
            _log.Verbose($"Close Damage Bonus from Pit Fighter = {result:P2} with {skillPoints} skill points");
        }

        return result;
    }
}
