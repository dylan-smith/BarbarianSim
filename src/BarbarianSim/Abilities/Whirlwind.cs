using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Abilities;

public static class Whirlwind
{
    public const double FURY_COST = 25.0;

    public static bool CanUse(SimulationState state)
    {
        if (state.Player.Auras.Contains(Aura.WeaponCooldown))
        {
            return false;
        }

        if (state.Player.Auras.Contains(Aura.Whirlwinding))
        {
            return false;
        }

        return state.Player.Fury >= (FURY_COST * FuryCostReductionCalculator.Calculate(state));
    }

    public static bool CanRefresh(SimulationState state) => state.Player.Fury >= (FURY_COST * FuryCostReductionCalculator.Calculate(state));

    public static void Use(SimulationState state) => state.Events.Add(new WhirlwindStartedEvent(state.CurrentTime));

    public static GearItem Weapon { get; set; }

    public static double GetSkillMultiplier(SimulationState state)
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
