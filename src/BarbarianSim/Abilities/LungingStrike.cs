using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public static class LungingStrike
{
    public const double LUCKY_HIT_CHANCE = 0.5;

    // Lunge forward and strike an enemy for 33% damage
    public static bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.WeaponCooldown);

    public static void Use(SimulationState state, EnemyState target) => state.Events.Add(new LungingStrikeEvent(state.CurrentTime, target));

    public static GearItem Weapon { get; set; }

    public static double GetSkillMultiplier(SimulationState state)
    {
        var skillPoints = state?.Config.Skills[Skill.LungingStrike];
        skillPoints += state?.Config.Gear.AllGear.Sum(g => g.LungingStrike);

        return skillPoints switch
        {
            1 => 0.33,
            2 => 0.36,
            3 => 0.39,
            4 => 0.42,
            >= 5 => 0.45,
            _ => 0.0,
        };
    }
}
