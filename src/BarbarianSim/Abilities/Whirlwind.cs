using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Abilities;

public static class Whirlwind
{
    public const double FURY_COST = 25.0;
    public const double LUCKY_HIT_CHANCE = 0.2;

    public static bool CanUse(SimulationState state)
    {
        return !state.Player.Auras.Contains(Aura.WeaponCooldown) &&
               !state.Player.Auras.Contains(Aura.Whirlwinding) &&
               state.Player.Fury >= (FURY_COST * FuryCostReductionCalculator.Calculate(state));
    }

    public static bool CanRefresh(SimulationState state) => state.Player.Fury >= (FURY_COST * FuryCostReductionCalculator.Calculate(state)) && state.Player.Auras.Contains(Aura.Whirlwinding);

    public static void Use(SimulationState state) => state.Events.Add(new WhirlwindStartedEvent(state.CurrentTime));

    public static void StopSpinning(SimulationState state) => state.Player.Auras.Remove(Aura.Whirlwinding);

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
