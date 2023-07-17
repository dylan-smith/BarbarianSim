using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class HeavyHanded
{
    // While using 2-Handed weapons you deal X%[x] increased Critical Strike Damage

    public virtual double GetCriticalStrikeDamage(SimulationState state, Expertise expertise)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.HeavyHanded) && expertise is Expertise.TwoHandedAxe or Expertise.TwoHandedMace or Expertise.TwoHandedSword or Expertise.Polearm)
        {
            skillPoints += state.Config.Skills[Skill.HeavyHanded];
        }

        return skillPoints switch
        {
            1 => 5,
            2 => 10,
            >= 3 => 15,
            _ => 0,
        };
    }
}
