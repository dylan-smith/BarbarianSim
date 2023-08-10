using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class PowerWarCry
{
    // If at least 6 enemies are Nearby when you use War Cry, your damage bonus is increased by an additional 10%[x]
    public const double DAMAGE_BONUS = 1.1;
    public const double NEARBY_ENEMIES = 6;

    public PowerWarCry(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double GetDamageBonus(SimulationState state)
    {
        if (state.Config.HasSkill(Skill.PowerWarCry) &&
            state.Enemies.Count >= NEARBY_ENEMIES &&
            state.Player.Auras.Contains(Aura.WarCry))
        {
            _log.Verbose($"Damage Bonus from Power War Cry = {DAMAGE_BONUS:F2}x");
            return DAMAGE_BONUS;
        }

        return 1.0;
    }
}
