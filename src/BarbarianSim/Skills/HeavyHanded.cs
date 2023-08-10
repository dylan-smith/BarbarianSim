using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class HeavyHanded
{
    // While using 2-Handed weapons you deal X%[x] increased Critical Strike Damage
    public HeavyHanded(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetCriticalStrikeDamage(SimulationState state, Expertise expertise)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.HeavyHanded) && expertise.IsTwoHanded())
        {
            skillPoints += state.Config.Skills[Skill.HeavyHanded];
        }

        var result = skillPoints switch
        {
            1 => 5,
            2 => 10,
            >= 3 => 15,
            _ => 0,
        };

        if (result > 0)
        {
            _log.Verbose($"Critical Strike Damage Bonus from Heavy Handed = {result:F2}% with {skillPoints} skill points");
        }

        return result;
    }
}
