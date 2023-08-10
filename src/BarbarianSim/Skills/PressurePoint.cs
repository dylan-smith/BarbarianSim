﻿using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class PressurePoint : IHandlesEvent<LuckyHitEvent>
{
    // Lucky Hit: Your Core skills have up to a 30% chance to make enemies Vulnerable for 2 seconds
    public PressurePoint(RandomGenerator randomGenerator, SimLogger log)
    {
        _randomGenerator = randomGenerator;
        _log = log;
    }

    public const double VULNERABLE_DURATION = 2.0;

    private readonly RandomGenerator _randomGenerator;
    private readonly SimLogger _log;

    public void ProcessEvent(LuckyHitEvent e, SimulationState state)
    {
        if (e.SkillType == SkillType.Core)
        {
            var roll = _randomGenerator.Roll(RollType.PressurePoint);

            if (roll <= GetProcPercentage(state))
            {
                state.Events.Add(new PressurePointProcEvent(e.Timestamp, e.Target));
                _log.Verbose($"Pressure Point created PressurePointProcEvent on Enemy #{e.Target.Id}");
            }
        }
    }

    public virtual double GetProcPercentage(SimulationState state)
    {
        var skillPoints = state.Config.GetSkillPoints(Skill.PressurePoint);
        var result = skillPoints switch
        {
            1 => 0.1,
            2 => 0.2,
            >= 3 => 0.3,
            _ => 0.0,
        };

        if (result > 0)
        {
            _log.Verbose($"Pressure Point proc chance is {result:P0}");
        }

        return result;
    }
}
