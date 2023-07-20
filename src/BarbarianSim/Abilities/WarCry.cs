using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.Abilities;

public class WarCry
{
    public const double DURATION = 6.0;
    public const double COOLDOWN = 25.0;
    public const double BERSERKING_DURATION_FROM_ENHANCED = 4.0;

    // Bellow a mighty war cry, increasing your damage dealt by 15%[x] for 6.0 seconds, and Nearby allies for 3.0 seconds (Cooldown: 25 seconds)
    // Enhanced: War Cry grants you Berserking for 4 seconds
    // Mighty: War Cry grants you 15%[x] Base Life (15%[x] HP) as Fortify

    public WarCry(PowerWarCry powerWarCry) => _powerWarCry = powerWarCry;

    private readonly PowerWarCry _powerWarCry;

    public virtual bool CanUse(SimulationState state) => !state.Player.Auras.Contains(Aura.WarCryCooldown);

    public virtual void Use(SimulationState state) => state.Events.Add(new WarCryEvent(state.CurrentTime));

    public virtual double GetDamageBonus(SimulationState state)
    {
        if (!state.Player.Auras.Contains(Aura.WarCry))
        {
            return 0;
        }

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

        damageBonus += _powerWarCry.GetDamageBonus(state);

        return damageBonus;
    }
}
