using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class PressurePoint
{
    public static void ProcessEvent(LuckyHitEvent e, SimulationState state)
    {
        if (e.SkillType == SkillType.Core)
        {
            var roll = RandomGenerator.Roll(RollType.PressurePoint);

            if (roll <= 0.3)
            {
                //state.Events.Add(new PressurePointProcEvent(e.Timestamp));
            }
        }
    }
}
