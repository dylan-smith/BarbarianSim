using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class HeavyHanded
{
    // While using 2-Handed weapons you deal 5%[x] increased Critical Strike Damage

    public double GetCriticalStrikeDamage(SimulationState state, Expertise expertise)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.HeavyHanded) && expertise is Expertise.TwoHandedAxe or Expertise.TwoHandedMace or Expertise.TwoHandedSword or Expertise.Polearm)
        {
            skillPoints += state.Config.Skills[Skill.HeavyHanded];
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
