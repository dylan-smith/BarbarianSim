using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Abilities;

public class Whirlwind
{
    public const double FURY_COST = 25.0;
    public const double LUCKY_HIT_CHANCE = 0.2;

    public Whirlwind(FuryCostReductionCalculator furyCostReductionCalculator,
                     WhirlwindSpinEventFactory whirlwindSpinEventFactory,
                     AuraExpiredEventFactory auraExpiredEventFactory)
    {
        _furyCostReductionCalculator = furyCostReductionCalculator;
        _whirlwindSpinEventFactory = whirlwindSpinEventFactory;
        _auraExpiredEventFactory = auraExpiredEventFactory;
    }

    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;
    private readonly WhirlwindSpinEventFactory _whirlwindSpinEventFactory;
    private readonly AuraExpiredEventFactory _auraExpiredEventFactory;

    public bool CanUse(SimulationState state)
    {
        return !state.Player.Auras.Contains(Aura.WeaponCooldown) &&
               !state.Player.Auras.Contains(Aura.Whirlwinding) &&
               state.Player.Fury >= (FURY_COST * _furyCostReductionCalculator.Calculate(state, SkillType.Core));
    }

    public bool CanRefresh(SimulationState state) => state.Player.Fury >= (FURY_COST * _furyCostReductionCalculator.Calculate(state, SkillType.Core)) && state.Player.Auras.Contains(Aura.Whirlwinding);

    public void Use(SimulationState state) => state.Events.Add(_whirlwindSpinEventFactory.Create(state.CurrentTime));

    public void StopSpinning(SimulationState state)
    {
        state.Events.Add(_auraExpiredEventFactory.Create(state.CurrentTime, Aura.Whirlwinding));
    }

    public GearItem Weapon { get; set; }

    public double GetSkillMultiplier(SimulationState state)
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
