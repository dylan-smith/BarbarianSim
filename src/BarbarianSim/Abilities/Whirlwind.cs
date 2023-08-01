using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Abilities;

public class Whirlwind
{
    public const double FURY_COST = 25.0;
    public const double LUCKY_HIT_CHANCE = 0.2;

    public Whirlwind(FuryCostReductionCalculator furyCostReductionCalculator) => _furyCostReductionCalculator = furyCostReductionCalculator;

    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;

    public virtual bool CanUse(SimulationState state)
    {
        return state.Config.HasSkill(Skill.Whirlwind) &&
               !state.Player.Auras.Contains(Aura.WeaponCooldown) &&
               !state.Player.Auras.Contains(Aura.Whirlwinding) &&
               state.Player.Fury >= (FURY_COST * _furyCostReductionCalculator.Calculate(state, SkillType.Core));
    }

    public virtual bool CanRefresh(SimulationState state) => state.Player.Fury >= (FURY_COST * _furyCostReductionCalculator.Calculate(state, SkillType.Core)) && state.Player.Auras.Contains(Aura.Whirlwinding);

    public virtual void Use(SimulationState state) => state.Events.Add(new WhirlwindSpinEvent(state.CurrentTime));

    public virtual void StopSpinning(SimulationState state)
    {
        state.Events.Add(new AuraExpiredEvent(state.CurrentTime, Aura.Whirlwinding));
    }

    public GearItem Weapon { get; set; }

    public virtual double GetSkillMultiplier(SimulationState state)
    {
        var skillPoints = state?.Config.Skills[Skill.Whirlwind];
        skillPoints += state?.Config.Gear.AllGear.Sum(g => g.Whirlwind);

        return skillPoints switch
        {
            1 => 0.17,
            2 => 0.19,
            3 => 0.21,
            4 => 0.23,
            >= 5 => 0.24,
            _ => 0.0,
        };
    }
}
