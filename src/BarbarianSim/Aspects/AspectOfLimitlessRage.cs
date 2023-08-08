using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfLimitlessRage : Aspect, IHandlesEvent<FuryGeneratedEvent>
{
    // Each point of Fury you generate while at Maximum Fury grants your next Core skill 1-2%[x] increased damage, up to 15-30%[x]
    public int Damage { get; set; }
    public int MaxDamage { get; set; }

    public AspectOfLimitlessRage(SimLogger log) => _log = log;

    private readonly SimLogger _log;
    private double _damageBoost;

    public void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        var originalBoost = _damageBoost;
        _damageBoost += e.OverflowFury * Damage;
        _damageBoost = Math.Min(MaxDamage, _damageBoost);

        if (_damageBoost != originalBoost)
        {
            _log.Verbose($"Aspect of Limitless Rage increased it's damage boost of next Core skill to {_damageBoost:F2}%");
        }
    }

    public virtual double GetDamageBonus(SimulationState state, SkillType skillType)
    {
        if (IsAspectEquipped(state) && skillType == SkillType.Core)
        {
            var result = 1 + (_damageBoost / 100.0);
            _damageBoost = 0;
            _log.Verbose($"Aspect of Limitless Rage granted {result:F2}x damage bonus");
            _log.Verbose($"Aspect of Limitless Rage reset to 0");
            return result;
        }

        return 1.0;
    }
}
