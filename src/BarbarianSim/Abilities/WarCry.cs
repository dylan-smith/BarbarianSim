using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Abilities;

public class WarCry
{
    public const double DURATION = 6.0;
    public const double COOLDOWN = 25.0;
    public const double BERSERKING_DURATION_FROM_ENHANCED = 4.0;

    // Bellow a mighty war cry, increasing your damage dealt by 15%[x] for 6.0 seconds, and Nearby allies for 3.0 seconds (Cooldown: 25 seconds)
    // Enhanced: War Cry grants you Berserking for 4 seconds
    // Mighty: War Cry grants you 15%[x] Base Life (15%[x] HP) as Fortify

    public WarCry(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual bool CanUse(SimulationState state) =>
        state.Config.HasSkill(Skill.WarCry)
        && !state.Player.Auras.Contains(Aura.WarCryCooldown);

    public virtual void Use(SimulationState state) => state.Events.Add(new WarCryEvent(state.CurrentTime));

    public virtual double GetDamageBonus(SimulationState state)
    {
        if (!state.Player.Auras.Contains(Aura.WarCry))
        {
            return 1.0;
        }

        var skillPoints = state.Config.Gear.AllGear.Sum(g => g.WarCry);
        skillPoints += state.Config.GetSkillPoints(Skill.WarCry);

        var result = skillPoints switch
        {
            1 => 1.15,
            2 => 1.165,
            3 => 1.18,
            4 => 1.195,
            >= 5 => 1.21,
            _ => 1.0,
        };

        _log.Verbose($"Damage bonus from War Cry = {result}x with {skillPoints} skill points");
        return result;
    }
}
