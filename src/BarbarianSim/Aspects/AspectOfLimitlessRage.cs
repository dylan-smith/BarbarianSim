using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfLimitlessRage : Aspect, IHandlesEvent<FuryGeneratedEvent>
{
    // Each point of Fury you generate while at Maximum Fury grants your next Core skill 1-2%[x] increased damage, up to 15-30%[x]
    public int Damage { get; set; }
    public int MaxDamage { get; set; }

    private double _damageBoost;

    public void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        _damageBoost += e.OverflowFury * Damage;
        _damageBoost = Math.Min(MaxDamage, _damageBoost);
    }

    public virtual double GetDamageBonus(SimulationState state, SkillType skillType)
    {
        if (IsAspectEquipped(state) && skillType == SkillType.Core)
        {
            var result = 1 + (_damageBoost / 100.0);
            _damageBoost = 0;
            return result;
        }

        return 1.0;
    }
}
