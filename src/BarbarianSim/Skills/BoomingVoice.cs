using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class BoomingVoice
{
    // Your Shout skill effect durations are increased by 24%[x]
    public virtual double GetDurationIncrease(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.BoomingVoice))
        {
            skillPoints += state.Config.Skills[Skill.BoomingVoice];
        }

        return skillPoints switch
        {
            1 => 1.08,
            2 => 1.16,
            >= 3 => 1.24,
            _ => 1,
        };
    }
}
