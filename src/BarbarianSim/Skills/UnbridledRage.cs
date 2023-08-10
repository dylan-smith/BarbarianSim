using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class UnbridledRage
{
    // Core skills deal 135%[x] increased damage, but cost 100%[x] more Fury
    public const double DAMAGE_BONUS = 2.35;
    public const double FURY_COST = 2.0;

    public UnbridledRage(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetFuryCostReduction(SimulationState state, SkillType skillType)
    {
        if (state.Config.HasSkill(Skill.UnbridledRage))
        {
            if (skillType == SkillType.Core)
            {
                _log.Verbose($"Fury Cost Increase from Unbridled Rage = {FURY_COST}x");
                return FURY_COST;
            }
        }

        return 1.0;
    }

    public virtual double GetDamageBonus(SimulationState state, SkillType skillType)
    {
        if (state.Config.HasSkill(Skill.UnbridledRage))
        {
            if (skillType == SkillType.Core)
            {
                _log.Verbose($"Damage Bonus from Unbridled Rage = {DAMAGE_BONUS:F2}x");
                return DAMAGE_BONUS;
            }
        }

        return 1.0;
    }
}
