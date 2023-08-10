using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class BoomingVoice
{
    // Your Shout skill effect durations are increased by 24%[x]
    public BoomingVoice(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetDurationIncrease(SimulationState state)
    {
        var skillPoints = 0;

        if (state.Config.Skills.ContainsKey(Skill.BoomingVoice))
        {
            skillPoints += state.Config.Skills[Skill.BoomingVoice];
        }

        var result = skillPoints switch
        {
            1 => 1.08,
            2 => 1.16,
            >= 3 => 1.24,
            _ => 1,
        };

        if (result > 1)
        {
            _log.Verbose($"Duration Increase from Booming Voice = {result:F2}x with {skillPoints} skill points");
        }

        return result;
    }
}
