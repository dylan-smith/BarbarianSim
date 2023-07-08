using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public static class IronSkin
{
    public const double DURATION = 5;
    public const double COOLDOWN = 14;
    public const double BONUS_FROM_ENHANCED = 0.2;
    public const double HEAL_FROM_TACTICAL = 0.1;
    public const double FORTIFY_FROM_STRATEGIC = 0.15;

    // Steel yourself, gaining a Barrier that absorbs 65% of your missing life for 5 seconds (Cooldown: 14 seconds)
    // Enhanced: Ironskin's Barrier absorbs 20% more of your Maximum Life
    // Tactical: While Ironskin is active Heal for 10% of the Barrier's original amount as Life per second
    // Strategic: Ironskin also grants 15% Base Life (15%[x] HP) as Fortify. Double this amount if cast while below 50% Life
    public static bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.IronSkinCooldown);

    public static void Use(SimulationState state) => state.Events.Add(new IronSkinEvent(state.CurrentTime));

    public static double GetBarrierPercentage(SimulationState state)
    {
        var skillPoints = state.Config.Gear.AllGear.Sum(g => g.IronSkin);

        if (state.Config.Skills.TryGetValue(Skill.IronSkin, out var pointsSpent))
        {
            skillPoints += pointsSpent;
        }

        return skillPoints switch
        {
            1 => 0.50,
            2 => 0.55,
            3 => 0.60,
            4 => 0.65,
            >= 5 => 0.70,
            _ => 0.0,
        };
    }
}
