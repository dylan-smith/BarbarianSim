using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class PowerWarCry
{
    // If at least 6 enemies are Nearby when you use War Cry, your damage bonus is increased by an additional 10%[x]
    public const double DAMAGE_BONUS_FROM_POWER = 0.1;
    public const double NEARBY_ENEMIES_FOR_POWER = 6;

    public virtual double GetDamageBonus(SimulationState state)
    {
        return state.Config.Skills.TryGetValue(Skill.PowerWarCry, out var skillPoints) &&
            skillPoints > 0 &&
            state.Enemies.Count >= NEARBY_ENEMIES_FOR_POWER &&
            state.Player.Auras.Contains(Aura.WarCry)
            ? DAMAGE_BONUS_FROM_POWER
            : 0;
    }
}
