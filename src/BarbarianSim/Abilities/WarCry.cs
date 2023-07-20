using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public class WarCry
{
    public const double DURATION = 6.0;
    public const double COOLDOWN = 25.0;
    public const double BERSERKING_DURATION_FROM_ENHANCED = 4.0;
    
    public const double DAMAGE_BONUS_FROM_POWER = 0.1;
    public const double NEARBY_ENEMIES_FOR_POWER = 6;

    // Bellow a mighty war cry, increasing your damage dealt by 15%[x] for 6.0 seconds, and Nearby allies for 3.0 seconds (Cooldown: 25 seconds)
    // Enhanced: War Cry grants you Berserking for 4 seconds
    // Mighty: War Cry grants you 15%[x] Base Life (15%[x] HP) as Fortify
    // Power: If at least 6 enemies are Nearby when you use War Cry, your damage bonus is increased by an additional 10%[x]
    public virtual bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.WarCryCooldown);

    public virtual void Use(SimulationState state) => state.Events.Add(new WarCryEvent(state.CurrentTime));

    public virtual double GetDamageBonus(SimulationState state)
    {
        var skillPoints = state.Config.Gear.AllGear.Sum(g => g.WarCry);

        if (state.Config.Skills.TryGetValue(Skill.WarCry, out var pointsSpent))
        {
            skillPoints += pointsSpent;
        }

        var damageBonus = skillPoints switch
        {
            1 => 1.15,
            2 => 1.165,
            3 => 1.18,
            4 => 1.195,
            >= 5 => 1.21,
            _ => 1.0,
        };

        if (state.Config.Skills.ContainsKey(Skill.PowerWarCry) && state.Enemies.Count >= NEARBY_ENEMIES_FOR_POWER)
        {
            damageBonus += DAMAGE_BONUS_FROM_POWER;
        }

        return damageBonus;
    }
}
