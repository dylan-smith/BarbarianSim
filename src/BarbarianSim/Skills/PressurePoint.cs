using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class PressurePoint
{
    // Lucky Hit: Your Core skills have up to a 30% chance to make enemies Vulnerable for 2 seconds
    public const double VULNERABLE_DURATION = 2.0;

    public void ProcessEvent(LuckyHitEvent e, SimulationState state)
    {
        if (e.SkillType == SkillType.Core)
        {
            var roll = RandomGenerator.Roll(RollType.PressurePoint);

            if (roll <= GetProcPercentage(state))
            {
                state.Events.Add(new PressurePointProcEvent(e.Timestamp, e.Target));
            }
        }
    }

    public double GetProcPercentage(SimulationState state)
    {
        if (state.Config.Skills.TryGetValue(Skill.PressurePoint, out var value))
        {
            value += state.Config.Gear.AllGear.Sum(g => g.PressurePoint);

            return value switch
            {
                1 => 0.1,
                2 => 0.2,
                >= 3 => 0.3,
                _ => 0.0,
            };
        }

        return 0;
    }
}
